use MU_Store
create table ClothesImages (
		ClothesImagesID int primary key identity(1,1),
		FileId uniqueidentifier rowguidcol not null unique,
		ImageFile varbinary(max) filestream,
		ClothesId int foreign key references Clothes(Article)
);


use MU_Store
ALTER TABLE Clothes ADD ImageID int

alter table ClothesImages add foreign key (Article) references Clothes(Article)

insert into ClothesImages(ClothesImagesID, FileId,ImageFile)
values (1, newid(), (select * from openrowset (bulk 'D:\Dima\00251561_00.jpg', single_blob) as image))

drop table ClothesImages

ALTER TABLE ClothesImages ADD CONSTRAINT [DF_MapMakerFiles_FileId] DEFAULT (newid()) FOR [FileId]

ALTER TABLE ClothesImages.[MapStudioFiles] ADD CONSTRAINT [PK_MapMakerFiles] PRIMARY KEY CLUSTERED ([FileId] ASC)