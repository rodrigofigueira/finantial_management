IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'categories')
BEGIN
    TRUNCATE TABLE categories;
END
