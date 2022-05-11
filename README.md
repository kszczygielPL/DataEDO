Program can load/save ToDo list from database or file. 

General functions of the program:
- the program is divided into three sections: ToDo list with a search engine, section for adding and editing entries, section for selecting the data storage method.
- ToDo list: shows a list of all entered data, allows you to search by title or description.
- Adding Editing: allows you to add and edit entries, releases buttons depending on what you can do Add always available except in edit mode, Edit available only if there are entries to be edited except in edit mode, Cancel available only in edit or add mode, Save available only after correct validating the form. After pressing "Save" the entry is added to the list but it is not saved in the storage, it is done only after pressing save data.
- Selecting data storage method: allows you to select the data storage method: Database or File
- When you try to close the form, the program checks if it is not in the editing or adding mode, and if all entries are saved
- Using DevExpress controls

Database MS SQL:
 - connection string by default is set to local machine, master database and logging with windows authorization. You can enter your own connection string which is slightly validated.
 - after pressing "Save data to destination.", the program adds the added entries and updates the changed ones.
 - the search by title or description retrieves the required data from the database using SQL
 - table used to store data
USE [master]
GO

/****** Object:  Table [dbo].[todolist]    Script Date: 11.05.2022 09:54:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[todolist](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nchar](150) NOT NULL,
	[description] [text] NOT NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_todolist_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

File:
- after selecting the form of saving to the file, you can download the name of the already created file or create a new one after selecting respectively "Open file" or "Create file".
- the entire ToDo list is always saved when working with files
- the search by title or description retrieves the required data from List<T> using LINQ
  
