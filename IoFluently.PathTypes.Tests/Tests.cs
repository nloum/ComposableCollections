using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoFluently.PathTypes.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void NonGenericFolderIsOtherTypesToo()
        {
            Folder folder = new Folder(null);
            IFileOrFolder fileOrFolder = folder;
            IFolderOrMissingPath folderOrMissingPath = folder;
            IFileOrFolderOrMissingPath fileOrFolderOrMissingPath = folder;
        }

        [TestMethod]
        public void GenericFolderIsOtherTypesToo()
        {
            Folder<Folder> folder = new Folder(null);
            IFileOrFolder<File, Folder> fileOrFolder = folder;
            IFolderOrMissingPath<Folder, MissingPath> folderOrMissingPath = folder;
            IFileOrFolderOrMissingPath<File, Folder, MissingPath> fileOrFolderOrMissingPath = folder;
        }

        [TestMethod]
        public void NonGenericFileIsOtherTypesToo()
        {
            File file = new File(null);
            IFileOrFolder fileOrFolder = file;
            IFileOrMissingPath folderOrMissingPath = file;
            IFileOrFolderOrMissingPath fileOrFolderOrMissingPath = file;
        }
        
        [TestMethod]
        public void GenericFileIsOtherTypesToo()
        {
            File<File> file = new File(null);
            IFileOrFolder<File, Folder> fileOrFolder = file;
            IFileOrMissingPath<File, MissingPath> folderOrMissingPath = file;
            IFileOrFolderOrMissingPath<File, Folder, MissingPath> fileOrFolderOrMissingPath = file;
        }

        [TestMethod]
        public void NonGenericMissingPathIsOtherTypesToo()
        {
            MissingPath missingPath = new MissingPath(null);
            IFolderOrMissingPath folderOrMissingPath = missingPath;
            IFileOrMissingPath fileOrMissingPath = missingPath;
            IFileOrFolderOrMissingPath fileOrFolderOrMissingPath = missingPath;
        }
        
        [TestMethod]
        public void GenericMissingPathIsOtherTypesToo()
        {
            MissingPath<MissingPath> missingPath = new MissingPath(null);
            IFolderOrMissingPath<Folder, MissingPath> folderOrMissingPath = missingPath;
            IFileOrMissingPath<File, MissingPath> fileOrMissingPath = missingPath;
            IFileOrFolderOrMissingPath<File, Folder, MissingPath> fileOrFolderOrMissingPath = missingPath;
        }
    }
}