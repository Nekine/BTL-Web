Create Database KiwiFastfood
Go
Use KiwiFastfood
Go
--Drop table KhachHang
--Go
Create Table	KhachHang
(
	MaKH INT IDENTITY(1,1),
	HoTen nVarchar(50) NOT NULL,
	TaiKhoan Varchar(50) UNIQUE,
	MatKhau Varchar(50) NOT NULL,
	Email Varchar(100) UNIQUE,
	DiaChiKH nVarchar(200),
	DienThoaiKH Varchar(50),
	NgaySinh DATETIME
	CONSTRAINT PK_KhachHang PRIMARY KEY(MaKH)
)
Go
--Drop table Loai
--Go
Create Table	Loai
(
	MaLoai	Int Identity(1,1),
	TenLoai	Nvarchar(50)	Not Null,
	Constraint	Pk_Loai Primary Key(MaLoai),
)
Go
--Drop table MonAn
--Go
Create Table	MonAn
(
	MaMon	Int	Identity(1,1),
	TenMon	Nvarchar(100)	Not Null,
	GiaBan	Decimal,
	NoiDung	Nvarchar(Max),
	AnhDD	Varchar(50),
	SoLuongTon	Int,
	MaLoai	Int,
	Constraint	Pk_MonAn	Primary Key(MaMon),
	Constraint	Fk_Loai	Foreign Key(MaLoai) References	Loai(MaLoai)
)
Go
--Drop table DonDatHang
--Go
CREATE TABLE DonDatHang
(
	MaDonHang INT IDENTITY(1,1),
	DaThanhToan bit,
	TinhTrangGiaohang bit,
	NgayDat Datetime,
	NgayGiao Datetime,
	MaKH INT,
	CONSTRAINT FK_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
	CONSTRAINT PK_DonDatHang PRIMARY KEY (MaDonHang)
)
Go
--Drop table ChiTietDatHang
--Go
CREATE TABLE ChiTietDatHang
(
	MaDonHang INT,
	MaMon INT,
	SoLuong Int Check(SoLuong>0),
	DonGia Decimal(18,0) Check(DonGia>=0),
	CONSTRAINT PK_CTDatHang PRIMARY KEY(MaDonHang,MaMon),
	CONSTRAINT FK_DonHang FOREIGN KEY (MaDonHang) REFERENCES DonDatHang(MaDonHang),
	CONSTRAINT FK_MonAn FOREIGN KEY (MaMon) REFERENCES MonAn(MaMon)
)
Go
--Thêm dữ liệu:
---Loại
	Insert into Loai values (N' PIZZA')
	Insert into Loai values (N' MÌ Ý')
	Insert into Loai values (N' GÀ')
	Insert into Loai values (N' XÚC XÍCH')
	Insert into Loai values (N' NƯỚC UỐNG')
select *from Loai
---Món ăn
	---Pizza
	Insert into MonAn values (N' PIZZA 5 LOẠI THỊT THƯỢNG HẠNG',89000,N' Pizza 5 loại thịt thượng hạng rất ngon','Pizza2.jpg',7,1)
	Insert into MonAn values (N' PIZZA HẢI SẢN NHIỆT ĐỚI XỐT TIÊU',89000,N' Pizza hải sản nhiệt đới xốt tiêu rất ngon','Pizza1.jpg',5,1)
	---Mì Ý
	Insert into MonAn values (N' MÌ Ý HẢI SẢN ĐÚT LÒ',79000,N' Mì Ý hải sản đút lò rất ngon','Pasta1.jpg',4,2)
	Insert into MonAn values (N' Mì Ý RAU CỦ ĐÚT LÒ',79000,N' Mì Ý rau củ đút lò rất ngon','Pasta2.jpg',6,2)
	---Gà
	Insert into MonAn values (N' GÀ KHÔNG XƯƠNG BBQ',69000,N' Gà không xương BBQ rất ngon','Chicken1.jpg',11,3)
	Insert into MonAn values (N' GÀ TẨM BỘT PHÔ MAI',69000,N' Gà tẩm bột phô mai rất ngon','Chicken2.jpg',9,3)
	---Xúc xích
	Insert into MonAn values (N' XÚC XÍCH XÔNG KHÓI ĐÚC LÒ 4 MIẾNG',39000,N' Xúc xích xông khói đúc lò rất ngon','Xx1.jpg',6,4)
	Insert into MonAn values (N' XÚC XÍCH XÔNG KHÓI ĐÚC LÒ 8 MIẾNG',69000,N' Xúc xích xông khói đúc lò rất ngon','Xx2.jpg',7,4)
	---Nước uống
	Insert into MonAn values (N' CHAI COCA 390ML',20000,N' Coca rất ngon','Coca1.jpg',20,5)
	Insert into MonAn values (N' CHAI SPRITE 1.5L',39000,N' Sprite rất ngon','Sprite2.jpg',19,5)
select *from MonAn
--Dữ liệu cập nhật: Tài khoản quản trị
Create table Admin
(
	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) not null,
	Hoten nVarchar(50)
)
Insert into Admin values('admin','123456','Nguyen Dang Khoa')
Insert into Admin values('user','654321','Mr Khoa')
select *from Admin