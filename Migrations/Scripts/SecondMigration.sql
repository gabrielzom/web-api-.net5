IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [clients] (
    [client_id] INT NOT NULL IDENTITY,
    [name_and_surname] nvarchar(40) NULL,
    [register_of_physical_person] CHAR(11) NULL,
    [date_of_born] DATE NOT NULL,
    CONSTRAINT [PK_clients] PRIMARY KEY ([client_id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220513034524_FirstMigration', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [clients] ADD [genre] nvarchar(11) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220527023003_SecondMigration', N'5.0.10');
GO

COMMIT;
GO

