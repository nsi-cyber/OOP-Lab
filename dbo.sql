CREATE TABLE [dbo].[Table]
(
	[username] NCHAR(10) NOT NULL PRIMARY KEY, 
    [password] NCHAR(10) NOT NULL, 
    [name] NCHAR(10) NULL, 
    [email] NCHAR(10) NULL, 
    [phone] NCHAR(10) NULL, 
    [address] NCHAR(10) NULL, 
    [city] NCHAR(10) NULL, 
    [country] NCHAR(10) NULL, 
    [highscore] INT NOT NULL
)
