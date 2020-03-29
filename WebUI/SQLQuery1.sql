use MU_Store
create table ClothesImages (
		ClothesImagesID int primary key,
		FileId uniqueidentifier rowguidcol not null unique,
		ImageFile varbinary(max) filestream
);


use MU_Store
ALTER TABLE Clothes ADD ImageID int

alter table Clothes add foreign key (ImageID) references ClothesImages(ClothesImagesID)

insert into ClothesImages(ClothesImagesID, FileId,ImageFile)
values (1, newid(), (select * from openrowset (bulk 'D:\Dima\00251561_00.jpg', single_blob) as image))