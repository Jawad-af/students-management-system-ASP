CREATE TABLE [dbo].[student] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [firstName]   NVARCHAR (MAX) NOT NULL,
    [lastName]    NVARCHAR (MAX) NOT NULL,
    [age]         INT            NOT NULL,
    [currentYear] INT            NOT NULL,
    [isGraduated] BIT            NOT NULL,
    CONSTRAINT [PK_student] PRIMARY KEY CLUSTERED ([id] ASC)
);

