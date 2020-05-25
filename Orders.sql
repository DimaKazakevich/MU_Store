create table OrderDetails (Id int primary key identity(1,1)
							OrderId int foreign key references 
							
							ProductId int foreign key references Clothes(Article)	
							Quantity int
							)

create table Orders (Id int primary key identity(1,1),
					UserId nvarchar(128) foreign key references AspNetUsers(Id),
					DateTime datetime,
					TotalCost decimal(16, 2),
					Status nvarchar(60)
					)
use MU_Store
alter table OrderDetails add Size nvarchar(10)