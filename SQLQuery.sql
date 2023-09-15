CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
	Username VARCHAR(50) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
	CreatedOn DateTime NOT NULL DEFAULT GETDATE()
);

CREATE TABLE VideoInformation (
    VideoID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    VideoTitle NVARCHAR(255) NOT NULL,
    About NVARCHAR(MAX) NOT NULL,
    Tags NVARCHAR(MAX) NOT NULL,
    Language NVARCHAR(50) NOT NULL,
    Category NVARCHAR(max) NOT NULL,
	VideoLocation NVARCHAR(max) NOT NULL,
	UploadedOn NVARCHAR(MAX) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_User_Video FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

select * from VideoInformation
select * from Users

