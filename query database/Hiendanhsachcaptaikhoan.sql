/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [Ma_CanBo]
      ,[HoTen]
      ,[DoB]
      ,[DiaChiThuongTru]
      ,[Ma_PhuongXa_TT]
      ,[SoCCCD]
      ,[NgayCap]
      ,[NoiCap]
      ,[Ma_CoQuan_DonVi]
      ,[Ma_ChucVu_ChucDanh]
      ,[Ma_QuanHuyen]
      ,[Ma_TinhThanh]
      ,[Email]
      ,[Ten]
  FROM [KSTN].[dbo].[DM_CanBo]
INSERT INTO dbo.NV_CapTaiKhoan
(
    NguoiGui,
    NgayGui,
    TrangThai,
    NgayCap,
    NguoiCap,
    FileCap
)
VALUES
(   48,         -- NguoiGui - int
    GETDATE(), -- NgayGui - datetime
    1,      -- TrangThai - bit
    GETDATE(), -- NgayCap - datetime
    274,         -- NguoiCap - int
    'abc.xyz'         -- FileCap - varchar(100)
    )


		SELECT ctk.ID,  nguoigui.HoTen AS TenNguoiGui, ctk.NgayGui, nguoicap.HoTen AS TenNguoiCap, ctk.NgayCap FROM dbo.NV_CapTaiKhoan AS ctk 
	FULL JOIN dbo.DM_CanBo  AS nguoigui ON nguoigui.Ma_CanBo = ctk.NguoiGui
	FULL JOIN   dbo.DM_CanBo  AS nguoicap ON ctk.NguoiCap = nguoicap.Ma_CanBo
	
	WHERE ctk.ID IS NOT NULL