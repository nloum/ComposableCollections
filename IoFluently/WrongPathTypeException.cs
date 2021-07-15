using System;
using System.Linq;
using System.Text;
using UnitsNet;

namespace IoFluently
{
    public static class WrongPathTypeExceptionExtensions
    {
        public static WrongPathTypeException? AssertExpectedType(this IFileOrFolderOrMissingPath path,
            params PathType[] expectedTypes)
        {
            var fileMustBeEmpty = false;
        
            if (!path.FileSystem.CanEmptyDirectoriesExist)
            {
                switch (path.FileSystem.EmptyFolderMode)
                {
                    case EmptyFolderMode.FoldersNeverExist:
                        if (expectedTypes.Contains(PathType.Folder))
                        {
                            var tmpExpectedTypes = new PathType[expectedTypes.Length + 1];
                            Array.Copy(expectedTypes, tmpExpectedTypes, expectedTypes.Length);
                            tmpExpectedTypes[^1] = PathType.MissingPath;
                            expectedTypes=tmpExpectedTypes;
                        }
                        break;
                    case EmptyFolderMode.FoldersOnlyExistIfTheyContainFiles:
                        if (expectedTypes.Contains(PathType.Folder))
                        {
                            var tmpExpectedTypes = new PathType[expectedTypes.Length + 1];
                            Array.Copy(expectedTypes, tmpExpectedTypes, expectedTypes.Length);
                            tmpExpectedTypes[^1] = PathType.MissingPath;
                            expectedTypes=tmpExpectedTypes;
                        }
                        break;
                    case EmptyFolderMode.EmptyFilesAreFolders:
                        if (expectedTypes.Contains(PathType.Folder))
                        {
                            fileMustBeEmpty = true;
                        }
                        break;
                    case EmptyFolderMode.AllNonExistentPathsAreFolders:
                        if (expectedTypes.Contains(PathType.MissingPath))
                        {
                            var tmpExpectedTypes = new PathType[expectedTypes.Length + 1];
                            Array.Copy(expectedTypes, tmpExpectedTypes, expectedTypes.Length);
                            tmpExpectedTypes[^1] = PathType.Folder;
                            expectedTypes=tmpExpectedTypes;
                        }
                        break;
                }
            }
            
            var actualType = path.FileSystem.Type(path);
            if (expectedTypes.All(expectedType => expectedType != actualType))
            {
                throw new WrongPathTypeException(path, actualType, expectedTypes);
            }

            if (fileMustBeEmpty && actualType == PathType.File)
            {
                var fileSize = path.FileSystem.FileSize(new File(path));
                if (fileSize != Information.Zero)
                {
                    throw new WrongPathTypeException(path, actualType, expectedTypes, fileMustBeEmpty);
                }
            }

            return null;
        }
    }
    
    public class WrongPathTypeException : Exception
    {
        public WrongPathTypeException(IFileOrFolderOrMissingPath path, PathType actualType, params PathType[] expectedTypes) : base(CalculateMessage(path, actualType, expectedTypes))
        {
            
        }

        public WrongPathTypeException(IFileOrFolderOrMissingPath path, PathType actualType, PathType[] expectedTypes, bool fileMustBeEmpty) : base(CalculateMessage(path, actualType, expectedTypes, fileMustBeEmpty))
        {
            
        }
        
        private static string CalculateMessage(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath,
            PathType actualType, PathType[] expectedTypes, bool fileMustBeEmpty = false)
        {
            var expectedPathTypesString = "";
            if (expectedTypes.Length == 1)
            {
                expectedPathTypesString = expectedTypes[0].ToString();
            }
            else if (expectedTypes.Length == 0)
            {
                expectedPathTypesString = "???";
            }
            else
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("either a ");
                stringBuilder.Append(ToString(expectedTypes[0], fileMustBeEmpty));
                for (var i = 1; i < expectedTypes.Length - 1; i++)
                {
                    stringBuilder.Append(", ");
                    stringBuilder.Append(ToString(expectedTypes[i], fileMustBeEmpty));
                }

                stringBuilder.Append(" or a ");
                stringBuilder.Append(ToString(expectedTypes[^1], fileMustBeEmpty));
                
                expectedPathTypesString = stringBuilder.ToString();
            }

            return
                $"Expected {fileOrFolderOrMissingPath} to be a {expectedPathTypesString} but it is actually a {ToString(actualType, false)}";
        }

        private static string ToString(PathType pathType, bool fileMustBeEmpty)
        {
            switch (pathType)
            {
                case PathType.File:
                    if (fileMustBeEmpty)
                    {
                        return "empty file";
                    }

                    return "file";
                case PathType.Folder:
                    return "folder";
                case PathType.MissingPath:
                    return "missing path";
                default:
                    throw new InvalidOperationException($"Unexpected path type {pathType}");
            }
        }
    }
}