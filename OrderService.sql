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
CREATE TABLE [Orders] (
    [OrderId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [RestaurantId] uniqueidentifier NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId])
);

CREATE TABLE [OrderItems] (
    [OrderItemId] uniqueidentifier NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    [MenuItemId] uniqueidentifier NOT NULL,
    [ItemName] nvarchar(max) NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([OrderItemId]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE
);

CREATE TABLE [StatusHistories] (
    [StatusId] uniqueidentifier NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [ChangedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_StatusHistories] PRIMARY KEY ([StatusId]),
    CONSTRAINT [FK_StatusHistories_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE
);

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);

CREATE INDEX [IX_StatusHistories_OrderId] ON [StatusHistories] ([OrderId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507144408_InitialCreate', N'9.0.4');

COMMIT;
GO

