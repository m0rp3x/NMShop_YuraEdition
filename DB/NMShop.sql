DROP SCHEMA IF EXISTS "NMShop" CASCADE;
CREATE SCHEMA "NMShop";
SET search_path = "NMShop";

CREATE TABLE IF NOT EXISTS "ProductImages" (
	"Id" serial NOT NULL UNIQUE,
	"Bytes" bytea NOT NULL,
	"Product_Id" integer NOT NULL,
	"IsMain" boolean NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Product" (
	"Id" serial NOT NULL UNIQUE,
	"Name" varchar(150) NOT NULL,
	"Brand_Id" integer NOT NULL,
	"Article" varchar(150) NOT NULL,
	"Description" varchar(1000) NOT NULL,
	"Gender_Id" integer NOT NULL,
	"ProductType_Id" integer NOT NULL,
	"SellingCategory_Id" integer NOT NULL,
	"DateAdded" date NOT NULL,
	"Color_Id" integer NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Brands" (
	"Id" serial NOT NULL UNIQUE,
	"Name" varchar(100) NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Genders" (
	"Id" serial NOT NULL UNIQUE,
	"Name" varchar(50) NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "SellingCategories" (
	"Id" serial NOT NULL UNIQUE,
	"Name" varchar(50) NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "ProductTypes" (
	"Id" serial NOT NULL UNIQUE,
	"Name" varchar(50) NOT NULL,
	"ParentType_Id" integer,
    "SizeDisplayType" varchar(10),
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "ProductColors" (
	"Id" serial NOT NULL UNIQUE,
	"Value" varchar(6) NOT NULL,
	"Name" varchar(30) NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "StockInfo" (
	"Id" serial NOT NULL UNIQUE,
	"Product_Id" integer NOT NULL,
	"Size" numeric(10,0) NOT NULL,
	"Price" numeric(10,0) NOT NULL,
	"DiscountPrice" numeric(10,0),
	"AmountInStock" integer NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Orders" (
	"Id" serial NOT NULL UNIQUE,
	"ClientFullName" varchar(250) NOT NULL,
	"ClientPhone" varchar(11) NOT NULL,
	"DeliveryAdress" varchar(500) NOT NULL,
	"DeliveryType_Id" integer NOT NULL,
	"PaymentType_Id" integer NOT NULL,
	"OrderStatus_Id" integer NOT NULL,
	"DeliveryRecipientFullName" varchar(250) NOT NULL,
	"DeliveryRecipientPhone" varchar(11) NOT NULL,
	"Comment" varchar(1000) NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "DeliveryTypes" (
	"Id" serial NOT NULL UNIQUE,
	"Name" varchar(100) NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "PaymentTypes" (
	"Id" serial NOT NULL UNIQUE,
	"Name" varchar(100) NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "OrderStatuses" (
	"Id" serial NOT NULL UNIQUE,
	"Name" varchar(100) NOT NULL,
	PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "OrderParts" (
	"Id" serial NOT NULL UNIQUE,
	"Order_Id" integer NOT NULL,
	"Product_Id" integer NOT NULL,
	"Amount" integer NOT NULL,
	PRIMARY KEY ("Id")
);

ALTER TABLE "ProductImages" ADD CONSTRAINT "fk_ProductImages_Product_Id" FOREIGN KEY ("Product_Id") REFERENCES "Product"("Id");
ALTER TABLE "Product" ADD CONSTRAINT "fk_Product_Brand_Id" FOREIGN KEY ("Brand_Id") REFERENCES "Brands"("Id");
ALTER TABLE "Product" ADD CONSTRAINT "fk_Product_Gender_Id" FOREIGN KEY ("Gender_Id") REFERENCES "Genders"("Id");
ALTER TABLE "Product" ADD CONSTRAINT "fk_Product_ProductType_Id" FOREIGN KEY ("ProductType_Id") REFERENCES "ProductTypes"("Id");
ALTER TABLE "Product" ADD CONSTRAINT "fk_Product_SellingCategory_Id" FOREIGN KEY ("SellingCategory_Id") REFERENCES "SellingCategories"("Id");
ALTER TABLE "Product" ADD CONSTRAINT "fk_Product_Color_Id" FOREIGN KEY ("Color_Id") REFERENCES "ProductColors"("Id");
ALTER TABLE "ProductTypes" ADD CONSTRAINT "fk_ProductTypes_ParentType_Id" FOREIGN KEY ("ParentType_Id") REFERENCES "ProductTypes"("Id");
ALTER TABLE "StockInfo" ADD CONSTRAINT "fk_StockInfo_Product_Id" FOREIGN KEY ("Product_Id") REFERENCES "Product"("Id");
ALTER TABLE "Orders" ADD CONSTRAINT "fk_Orders_DeliveryType_Id" FOREIGN KEY ("DeliveryType_Id") REFERENCES "DeliveryTypes"("Id");
ALTER TABLE "Orders" ADD CONSTRAINT "fk_Orders_PaymentType_Id" FOREIGN KEY ("PaymentType_Id") REFERENCES "PaymentTypes"("Id");
ALTER TABLE "Orders" ADD CONSTRAINT "fk_Orders_OrderStatus_Id" FOREIGN KEY ("OrderStatus_Id") REFERENCES "OrderStatuses"("Id");
ALTER TABLE "OrderParts" ADD CONSTRAINT "fk_OrderParts_Order_Id" FOREIGN KEY ("Order_Id") REFERENCES "Orders"("Id");
ALTER TABLE "OrderParts" ADD CONSTRAINT "fk_OrderParts_Product_Id" FOREIGN KEY ("Product_Id") REFERENCES "Product"("Id");

-- Insert test data into reference tables
INSERT INTO "Brands" ("Name") VALUES
  ('Nike'),
  ('Adidas'),
  ('Puma'),
  ('Asics'),
  ('Balenciaga'),
  ('Bape'),
  ('Bearbrick'),
  ('C.P.Company'),
  ('Chrome Hearts'),
  ('Converse'),
  ('Daniel Arsham'),
  ('Denim Tears'),
  ('Fear of God'),
  ('Golf Wang'),
  ('Goyard'),
  ('Louis Vuitton'),
  ('Maison Mihara Yasuhiro'),
  ('Medicom Toy'),
  ('Mozi'),
  ('New Balance'),
  ('Nike'),
  ('NIKITA EFREMOV'),
  ('Nothomme'),
  ('Palace'),
  ('Rick Owens'),
  ('Sacai'),
  ('Salomon'),
  ('Solemate'),
  ('Stone Island'),
  ('Supreme'),
  ('Swatch'),
  ('Takashi Murakami'),
  ('Tarrago'),
  ('The North Face'),
  ('Timberland'),
  ('Travis Scott'),
  ('TUD'),
  ('UGG'),
  ('UNIQLO'),
  ('Vans'),
  ('Vetements'),
  ('Vinyl'),
  ('Yeezy');

INSERT INTO "Genders" ("Name") VALUES ('Мужской'), ('Женский'), ('Унисекс');

INSERT INTO "SellingCategories" ("Name") VALUES
  ('Новые релизы'),
  ('Хиты продаж'),
  ('Коллаборации'),
  ('Эксклюзивы'),
  ('Маст-хэв');

INSERT INTO "ProductTypes" ("Name", "ParentType_Id", "SizeDisplayType") VALUES
  ('Одежда', NULL, 'string'),
  ('Куртки и пуховики', 1, NULL),
  ('Футболки и лонгсливы', 1, NULL),
  ('Штаны и джинсы', 1, NULL),
  ('Шорты', 1, NULL),
  ('Худи и свитшоты', 1, NULL),
  ('Бельё', 1, NULL),
  ('Аксессуары', NULL, 'none'),
  ('Головные уборы', 8, NULL),
  ('Рюкзаки и сумки', 8, NULL),
  ('Кошельки', 8, NULL),
  ('Очки', 8, NULL),
  ('Другие аксессуары', 8, NULL),
  ('Обувь', NULL, 'decimal'),
  ('Кеды и кроссовки', 14, NULL),
  ('Ботинки и угги', 14, NULL),
  ('Слайды', 14, NULL),
  ('Детское', 14, NULL);

INSERT INTO "ProductColors" ("Value", "Name") VALUES ('FF5733', 'Красный'), ('33FF57', 'Зелёный'), ('3357FF', 'Синий');

INSERT INTO "DeliveryTypes" ("Name") VALUES ('Курьер'), ('Самовывоз');

INSERT INTO "PaymentTypes" ("Name") VALUES ('Кредитная карта'), ('Наличные');

INSERT INTO "OrderStatuses" ("Name") VALUES ('В ожидании'), ('Завершён'), ('Отменён');

-- Add constraint to prevent deep inheritance chains in ProductTypes
CREATE OR REPLACE FUNCTION check_parent_depth()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW."ParentType_Id" IS NOT NULL THEN
        -- Проверяем, что у родительского типа нет собственного родителя
        IF EXISTS (
            SELECT 1 FROM "ProductTypes"
            WHERE "Id" = NEW."ParentType_Id" AND "ParentType_Id" IS NOT NULL
        ) THEN
            RAISE EXCEPTION 'Родительский тип не должен иметь собственного родителя.';
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER chk_ProductTypes_ParentDepth
BEFORE INSERT OR UPDATE ON "ProductTypes"
FOR EACH ROW
EXECUTE FUNCTION check_parent_depth();

-- Insert test data into Product table for each parent category
INSERT INTO "Product" ("Name", "Brand_Id", "Article", "Description", "Gender_Id", "ProductType_Id", "SellingCategory_Id", "DateAdded", "Color_Id") VALUES
  -- Одежда
  ('Куртка зимняя Nike', 1, 'JK001', 'Теплая зимняя куртка', 1, 2, 1, '2024-10-10', 1),
  ('Куртка Adidas', 2, 'JK002', 'Удобная куртка для холодной погоды', 2, 2, 1, '2024-10-11', 2),
  ('Футболка Puma', 3, 'TS001', 'Стильная футболка для повседневной носки', 1, 3, 1, '2024-10-12', 3),
  ('Джинсы UNIQLO', 38, 'JS001', 'Удобные джинсы', 1, 4, 1, '2024-10-13', 1),
  ('Шорты Supreme', 29, 'SH001', 'Летние шорты', 1, 5, 1, '2024-10-14', 2),
  ('Худи Swatch', 30, 'HD001', 'Теплое худи', 2, 6, 1, '2024-10-15', 3),
  ('Бельё Puma', 3, 'UL001', 'Комфортное бельё', 1, 7, 1, '2024-10-16', 1),
  ('Футболка Adidas', 2, 'TS002', 'Футболка для тренировок', 1, 3, 1, '2024-10-17', 2),
  ('Джинсы Levi', 38, 'JS002', 'Джинсы с классическим кроем', 2, 4, 1, '2024-10-18', 3),
  ('Худи Nike', 1, 'HD002', 'Худи с капюшоном', 1, 6, 1, '2024-10-19', 1),
  -- Аксессуары
  ('Головной убор Nike', 1, 'HT001', 'Спортивная шапка', 2, 9, 3, '2024-10-20', 2),
  ('Рюкзак The North Face', 33, 'BG001', 'Прочный рюкзак', 1, 10, 3, '2024-10-21', 3),
  ('Очки Gucci', 15, 'GL001', 'Модные солнцезащитные очки', 2, 12, 3, '2024-10-22', 1),
  ('Кошелёк Louis Vuitton', 16, 'WL001', 'Кожаный кошелёк', 1, 11, 3, '2024-10-23', 2),
  ('Рюкзак Supreme', 29, 'BG002', 'Рюкзак для города', 1, 10, 3, '2024-10-24', 3),
  ('Очки Ray-Ban', 15, 'GL002', 'Классические солнцезащитные очки', 2, 12, 3, '2024-10-25', 1),
  ('Головной убор Adidas', 2, 'HT002', 'Шапка для зимы', 1, 9, 3, '2024-10-26', 2),
  ('Кошелёк Gucci', 15, 'WL002', 'Кошелёк с монограммой', 2, 11, 3, '2024-10-27', 3),
  ('Рюкзак Vans', 40, 'BG003', 'Рюкзак для учебы', 1, 10, 3, '2024-10-28', 1),
  ('Очки Prada', 15, 'GL003', 'Очки в стиле ретро', 2, 12, 3, '2024-10-29', 2),
  -- Обувь
  ('Кеды Converse', 10, 'SN001', 'Классические кеды', 1, 15, 4, '2024-10-30', 3),
  ('Ботинки Timberland', 35, 'BT001', 'Прочные ботинки для зимы', 1, 16, 4, '2024-10-31', 1),
  ('Слайды Adidas', 2, 'SL001', 'Слайды для бассейна', 1, 17, 4, '2024-11-01', 2),
  ('Кеды Vans', 40, 'SN002', 'Кеды для скейтбординга', 1, 15, 4, '2024-11-02', 3),
  ('Ботинки UGG', 37, 'BT002', 'Теплые ботинки для холодной погоды', 2, 16, 4, '2024-11-03', 1),
  ('Слайды Nike', 1, 'SL002', 'Комфортные слайды', 1, 17, 4, '2024-11-04', 2),
  ('Кроссовки Balenciaga', 5, 'SN003', 'Модные кроссовки', 2, 15, 4, '2024-11-05', 3),
  ('Ботинки Dr. Martens', 38, 'BT003', 'Кожаные ботинки', 1, 16, 4, '2024-11-06', 1),
  ('Слайды Puma', 3, 'SL003', 'Удобные слайды для дома', 2, 17, 4, '2024-11-07', 2),
  ('Кеды Nike Air', 1, 'SN004', 'Спортивные кеды', 1, 15, 4, '2024-11-08', 3);

-- Добавляем блок для вставки данных в StockInfo
DO $$
DECLARE
    product_rec RECORD;
    product_cursor CURSOR FOR SELECT "Id", "ProductType_Id" FROM "Product";
    product_type_id INT;
    possible_sizes INT[];
    num_sizes INT;
    selected_sizes INT[];
    size INT;
    total_records_inserted INT := 0;
    price NUMERIC(10,2);
    discount_price NUMERIC(10,2);
    amount_in_stock integer;
BEGIN
    FOR product_rec IN product_cursor LOOP
        product_type_id := product_rec."ProductType_Id";

        -- Определяем возможные размеры на основе ProductType_Id
        IF product_type_id BETWEEN 2 AND 7 THEN
            -- Размеры одежды от XS (1) до XXL (6)
            possible_sizes := ARRAY[1,2,3,4,5,6];
        ELSIF product_type_id BETWEEN 9 AND 13 THEN
            -- Аксессуары, один размер
            possible_sizes := ARRAY[1];
        ELSIF product_type_id BETWEEN 15 AND 17 THEN
            -- Размеры обуви от 38 до 44
            possible_sizes := ARRAY[38,39,40,41,42,43,44];
        ELSE
            -- По умолчанию один размер
            possible_sizes := ARRAY[1];
        END IF;

        -- Случайное количество размеров от 1 до 7
        num_sizes := 1 + FLOOR(RANDOM() * LEAST(ARRAY_LENGTH(possible_sizes,1),7));

        -- Перемешиваем возможные размеры и выбираем нужное количество
        SELECT ARRAY_AGG(sizes ORDER BY RANDOM()) INTO selected_sizes FROM UNNEST(possible_sizes) AS sizes;
        selected_sizes := selected_sizes[1:num_sizes];

        -- Устанавливаем базовую цену
        price := 1000 + FLOOR(RANDOM() * 10000); -- Цена от 1000 до 11000

        -- Для каждого выбранного размера вставляем запись в StockInfo
        FOREACH size IN ARRAY selected_sizes LOOP
            total_records_inserted := total_records_inserted + 1;

            -- Определяем DiscountPrice
            IF MOD(total_records_inserted,7) = 0 THEN
                discount_price := price * 0.9; -- Скидка 10%
            ELSE
                discount_price := NULL;
            END IF;

            -- Определяем AmountInStock
            IF MOD(total_records_inserted,10) = 0 THEN
                amount_in_stock := 0; -- Нет в наличии
            ELSE
                amount_in_stock := 1 + FLOOR(RANDOM() * 20); -- Количество от 1 до 20
            END IF;

            INSERT INTO "StockInfo" ("Product_Id", "Size", "Price", "DiscountPrice", "AmountInStock")
            VALUES (product_rec."Id", size, price, discount_price, amount_in_stock);
        END LOOP;
    END LOOP;
END
$$;

-- Ваши предыдущие операции создания схемы, таблиц и вставки данных

-- Добавляем по три изображения для каждого товара
DO $$
DECLARE
    product_rec RECORD;
BEGIN
    FOR product_rec IN SELECT "Id" FROM "Product" LOOP
        -- Вставляем главное изображение
        INSERT INTO "ProductImages" ("Bytes", "Product_Id", "IsMain")
        VALUES (
            E''::bytea, -- пустое значение bytea
            product_rec."Id",
            TRUE
        );
        -- Вставляем два дополнительных изображения
        INSERT INTO "ProductImages" ("Bytes", "Product_Id", "IsMain")
        VALUES
            (E''::bytea, product_rec."Id", FALSE),
            (E''::bytea, product_rec."Id", FALSE);
    END LOOP;
END
$$;

CREATE TABLE tg_admins
(
    Id          SERIAL PRIMARY KEY,
    Username    VARCHAR(255) NOT NULL,
    TelegramId  VARCHAR(12) NOT NULL,
    CONSTRAINT tg_admins_username_uindex UNIQUE (Username)
);

ALTER TABLE tg_admins
    OWNER TO "NMShop_dollsawour";




