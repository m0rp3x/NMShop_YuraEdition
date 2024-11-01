DROP SCHEMA IF EXISTS "NMShop" CASCADE;
CREATE SCHEMA "NMShop";
SET search_path = "NMShop";
ALTER USER m0rp3x SET search_path = "NMShop";

CREATE TABLE IF NOT EXISTS "DeliveryTypes" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(100) NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "PaymentTypes" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(100) NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "OrderStatuses" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(100) NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "ContactMethods" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(100) NOT NULL UNIQUE,
    "ValidationMask" varchar(255),
    "ValidationErrorText" varchar(255),
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "PromoCodes" (
    "Id" serial NOT NULL UNIQUE,
    "Code" varchar(50) NOT NULL UNIQUE,
    "MaxUsages" integer NOT NULL,
    "DiscountPercent" integer NOT NULL,
    "ExpirationDate" date,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Orders" (
    "Id" serial NOT NULL UNIQUE,
    "ClientFullName" varchar(250) NOT NULL,
    "DeliveryAdress" varchar(500) NOT NULL,
    "DeliveryType_Id" integer NOT NULL,
    "PaymentType_Id" integer NOT NULL,
    "EstimatedDeliveryDateRange" varchar(50) DEFAULT 'Уточняем',
    "OrderStatus_Id" integer NOT NULL,
    "DeliveryRecipientFullName" varchar(250) NOT NULL,
    "DeliveryRecipientPhone" varchar(11) NOT NULL,
    "Comment" varchar(1000),
    "ContactMethod_Id" integer NOT NULL,
    "ContactValue" varchar(255) NOT NULL,
    "PromoCode_Id" integer,
    "Total" decimal ,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("DeliveryType_Id") REFERENCES "DeliveryTypes"("Id"),
    FOREIGN KEY ("PaymentType_Id") REFERENCES "PaymentTypes"("Id"),
    FOREIGN KEY ("OrderStatus_Id") REFERENCES "OrderStatuses"("Id"),
    FOREIGN KEY ("ContactMethod_Id") REFERENCES "ContactMethods"("Id"),
    FOREIGN KEY ("PromoCode_Id") REFERENCES "PromoCodes"("Id")
);

CREATE TABLE IF NOT EXISTS "Brands" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(100) NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Genders" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(50) NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "SellingCategories" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(50) NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "ProductTypes" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(50) NOT NULL UNIQUE,
    "ParentType_Id" integer,
    "SizeDisplayType" varchar(10),
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("ParentType_Id") REFERENCES "ProductTypes"("Id")
);

CREATE TABLE IF NOT EXISTS "ProductColors" (
    "Id" serial NOT NULL UNIQUE,
    "Value" varchar(6) NOT NULL UNIQUE,
    "Name" varchar(30) NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Product" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(150) NOT NULL,
    "Brand_Id" integer NOT NULL,
    "Article" varchar(150) NOT NULL UNIQUE,
    "Description" varchar(1000) NOT NULL,
    "Gender_Id" integer NOT NULL,
    "ProductType_Id" integer NOT NULL,
    "SellingCategory_Id" integer NOT NULL,
    "DateAdded" date NOT NULL,
    "Color_Id" integer NOT NULL,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("Brand_Id") REFERENCES "Brands"("Id"),
    FOREIGN KEY ("Gender_Id") REFERENCES "Genders"("Id"),
    FOREIGN KEY ("ProductType_Id") REFERENCES "ProductTypes"("Id"),
    FOREIGN KEY ("SellingCategory_Id") REFERENCES "SellingCategories"("Id"),
    FOREIGN KEY ("Color_Id") REFERENCES "ProductColors"("Id")
);

CREATE TABLE IF NOT EXISTS "ProductImages" (
    "Id" serial NOT NULL UNIQUE,
    "Bytes" bytea NOT NULL,
    "Product_Id" integer NOT NULL,
    "IsMain" boolean NOT NULL,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("Product_Id") REFERENCES "Product"("Id")
);

CREATE TABLE IF NOT EXISTS "StockInfo" (
    "Id" serial NOT NULL UNIQUE,
    "Product_Id" integer NOT NULL,
    "Size" numeric(10,0) NOT NULL,
    "Price" numeric(10,0) NOT NULL,
    "DiscountPrice" numeric(10,0),
    "AmountInStock" integer NOT NULL,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("Product_Id") REFERENCES "Product"("Id")
);

CREATE TABLE IF NOT EXISTS "OrderParts" (
    "Id" serial NOT NULL UNIQUE,
    "Order_Id" integer NOT NULL,
    "StockInfo_Id" integer NOT NULL,
    "Amount" integer NOT NULL,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("Order_Id") REFERENCES "Orders"("Id"),
    FOREIGN KEY ("StockInfo_Id") REFERENCES "StockInfo"("Id")
);

CREATE TABLE IF NOT EXISTS "NavigationItems" (
    "Id" serial NOT NULL UNIQUE,
    "Name" varchar(100) NOT NULL,
    "Link" varchar(255) NOT NULL,
    "ParentItem_Id" integer,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("ParentItem_Id") REFERENCES "NavigationItems"("Id")
);

CREATE TABLE IF NOT EXISTS "TextSizes" (
    "Id" serial NOT NULL UNIQUE,
    "Value" varchar(50) NOT NULL UNIQUE,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "ReferenceTopics" (
    "Id" serial NOT NULL UNIQUE,
    "Code" varchar(50) NOT NULL,
    "Name" varchar(100) NOT NULL,
    "ParentTopic_Id" integer,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("ParentTopic_Id") REFERENCES "ReferenceTopics"("Id")
);

CREATE TABLE IF NOT EXISTS "ReferenceContent" (
    "Id" serial NOT NULL UNIQUE,
    "Topic_Id" integer NOT NULL,
    "TextSize_Id" integer NOT NULL,
    "Content" varchar(1000) NOT NULL,
    "IsBold" boolean NOT NULL,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("Topic_Id") REFERENCES "ReferenceTopics"("Id"),
    FOREIGN KEY ("TextSize_Id") REFERENCES "TextSizes"("Id")
);

CREATE TABLE IF NOT EXISTS "BrandGalleryItems" (
    "Id" serial NOT NULL UNIQUE,
    "Brand_Id" integer NOT NULL,
    "Image" bytea NOT NULL,
    PRIMARY KEY ("Id"),
    FOREIGN KEY ("Brand_Id") REFERENCES "Brands"("Id")
);

CREATE TABLE IF NOT EXISTS "BannerCarouselItems" (
    "Id" serial NOT NULL UNIQUE,
    "Image" bytea NOT NULL,
    PRIMARY KEY ("Id")
);


CREATE TABLE Users (
    UserID SERIAL PRIMARY KEY,
    Username VARCHAR(50),
    TelegramID BIGINT UNIQUE,
    Role VARCHAR(20) DEFAULT 'Client',
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Admins (
    AdminID SERIAL PRIMARY KEY,
    Username VARCHAR(50),
    TelegramID BIGINT UNIQUE,
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Tickets (
    TicketID SERIAL PRIMARY KEY,
    UserID INT,
    AdminID INT NULL,
    Subject VARCHAR(255),
    Description TEXT,
    Status VARCHAR(20) DEFAULT 'Open',
    CreatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (AdminID) REFERENCES Admins(AdminID)
);

CREATE TABLE TicketMessages (
    MessageID SERIAL PRIMARY KEY,
    TicketID INT,
    UserID INT,
    AdminID INT NULL,
    Message TEXT,
    SentAt TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (TicketID) REFERENCES Tickets(TicketID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (AdminID) REFERENCES Admins(AdminID)
);

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

-- Добавляем проверку на количество использований промокода и его истечение при создании заказа
CREATE OR REPLACE FUNCTION check_promo_code_validity()
RETURNS TRIGGER AS $$
DECLARE
    promo_usage_count integer;
BEGIN
    IF NEW."PromoCode_Id" IS NOT NULL THEN
        -- Указываем схему NMShop явно при обращении к таблице Orders
        SELECT COUNT(*) INTO promo_usage_count
        FROM "Orders"
        WHERE "PromoCode_Id" = NEW."PromoCode_Id";

        -- Проверяем максимальное количество использований
        IF promo_usage_count >= (SELECT "MaxUsages" FROM "NMShop"."PromoCodes" WHERE "Id" = NEW."PromoCode_Id") THEN
            RAISE EXCEPTION 'Промокод достиг максимального количества использований.';
        END IF;

        -- Проверяем срок действия промокода
        IF (SELECT "ExpirationDate" FROM "NMShop"."PromoCodes" WHERE "Id" = NEW."PromoCode_Id") < CURRENT_DATE THEN
            RAISE EXCEPTION 'Срок действия промокода истек.';
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_check_promo_code_validity
BEFORE INSERT OR UPDATE ON "NMShop"."Orders"
FOR EACH ROW
EXECUTE FUNCTION check_promo_code_validity();

-- Добавляем проверку на максимальное наследование навигационного пункта
CREATE OR REPLACE FUNCTION check_navigation_depth()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW."ParentItem_Id" IS NOT NULL THEN
        IF EXISTS (
            SELECT 1 FROM "NavigationItems"
            WHERE "Id" = NEW."ParentItem_Id" AND "ParentItem_Id" IS NOT NULL
        ) THEN
            RAISE EXCEPTION 'Навигационный пункт не должен иметь собственного родителя.';
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER chk_NavigationItems_ParentDepth
BEFORE INSERT OR UPDATE ON "NavigationItems"
FOR EACH ROW
EXECUTE FUNCTION check_navigation_depth();

-- Добавляем проверку на максимальное наследование топика
CREATE OR REPLACE FUNCTION check_topic_depth()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW."ParentTopic_Id" IS NOT NULL THEN
        IF EXISTS (
            SELECT 1 FROM "ReferenceTopics"
            WHERE "Id" = NEW."ParentTopic_Id" AND "ParentTopic_Id" IS NOT NULL
        ) THEN
            RAISE EXCEPTION 'Топик справочной информации не должен иметь собственного родителя.';
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER chk_ReferenceTopics_ParentDepth
BEFORE INSERT OR UPDATE ON "ReferenceTopics"
FOR EACH ROW
EXECUTE FUNCTION check_topic_depth();

CREATE OR REPLACE FUNCTION check_brand_gallery_limit()
RETURNS TRIGGER AS $$
DECLARE
    brand_gallery_count integer;
BEGIN
    SELECT COUNT(*) INTO brand_gallery_count FROM "BrandGalleryItems" WHERE "Brand_Id" = NEW."Brand_Id";
    IF brand_gallery_count >= 3 THEN
        RAISE EXCEPTION 'Нельзя добавить больше 3 записей в галерею брендов.';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_check_brand_gallery_limit
BEFORE INSERT ON "BrandGalleryItems"
FOR EACH ROW
EXECUTE FUNCTION check_brand_gallery_limit();









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

INSERT INTO "OrderStatuses" ("Name") VALUES ('Создан'), ('В ожидании'), ('Завершён'), ('Отменён');

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


INSERT INTO "ContactMethods" ("Name", "ValidationMask", "ValidationErrorText")
VALUES
    ('Телефон', '^\d{10,11}$', 'Телефон должен содержать от 10 до 11 цифр.'),
    ('Telegram', '^@[A-Za-z0-9_]{5,32}$', 'Telegram имя пользователя должно начинаться с @ и содержать от 5 до 32 символов.'),
    ('WhatsApp', '^\d{10,11}$', 'WhatsApp номер должен содержать от 10 до 11 цифр.'),
    ('VK', '^https?:\/\/(www\.)?vk\.com\/[A-Za-z0-9_]+$', 'Введите корректную ссылку на профиль VK.');

INSERT INTO "PromoCodes" ("Code", "MaxUsages", "DiscountPercent", "ExpirationDate") VALUES
  ('TESTEXPIRED', 100, 30, '2023-01-01'),
  ('NEWYEAR2025', 100, 20, '2025-01-01'),
  ('SUMMER25', 50, 25, '2024-12-31'),
  ('WELCOME10', 500, 10, NULL);

INSERT INTO "TextSizes" ("Value") VALUES ('Маленький'), ('Средний'), ('Большой');

INSERT INTO "BrandGalleryItems" ("Brand_Id", "Image") VALUES
  (1, E''::bytea),
  (1, E''::bytea),
  (1, E''::bytea);

-- Главные категории
INSERT INTO "NavigationItems" ("Name", "Link", "ParentItem_Id")
VALUES
    ('Обувь', 'Shoes', NULL),
    ('Одежда', 'Clothing', NULL),
    ('Аксессуары', 'Accessories', NULL),
    ('Коллекции', 'Collections', NULL),
    ('Бренды', 'Brands', NULL),
    ('Информация', 'Info', NULL);

-- Субкатегории "Обувь"
INSERT INTO "NavigationItems" ("Name", "Link", "ParentItem_Id")
VALUES
    ('Смотреть всё', 'shoes-all', 1),
    ('Кеды и кроссовки', 'shoes-sneakers', 1),
    ('Ботинки и угги', 'shoes-boots', 1),
    ('Слайды', 'shoes-slides', 1),
    ('Детское', 'shoes-kids', 1);

-- Субкатегории "Одежда"
INSERT INTO "NavigationItems" ("Name", "Link", "ParentItem_Id")
VALUES
    ('Смотреть всё', 'clothing-all', 2),
    ('Ботинки и угги', 'clothing-boots', 2),
    ('Куртки и пуховики', 'clothing-jackets', 2),
    ('Футболки и лонгсливы', 'clothing-tshirts', 2),
    ('Штаны и джинсы', 'clothing-pants', 2),
    ('Шорты', 'clothing-shorts', 2),
    ('Худи и свитшоты', 'clothing-hoodies', 2),
    ('Белье', 'clothing-underwear', 2);

-- Субкатегории "Аксессуары"
INSERT INTO "NavigationItems" ("Name", "Link", "ParentItem_Id")
VALUES
    ('Смотреть всё', 'accessories-all', 3),
    ('Кошельки', 'accessories-wallets', 3),
    ('Очки', 'accessories-glasses', 3),
    ('Головные уборы', 'accessories-hats', 3),
    ('Рюкзаки и сумки', 'accessories-bags', 3),
    ('Другие аксессуары', 'accessories-other', 3);

-- Субкатегории "Коллекции"
INSERT INTO "NavigationItems" ("Name", "Link", "ParentItem_Id")
VALUES
    ('Смотреть всё', 'collections-all', 4),
    ('Bearbricks', 'collections-bearbricks', 4),
    ('Kaws', 'collections-kaws', 4),
    ('Фигурки', 'collections-figures', 4),
    ('Предметы интерьера', 'collections-furniture', 4),
    ('Другой арт', 'collections-other-art', 4);

-- Субкатегории "Бренды"
INSERT INTO "NavigationItems" ("Name", "Link", "ParentItem_Id")
VALUES
    ('GOYARD', 'brands-goyard', 5),
    ('YEEZY', 'brands-yeezy', 5),
    ('TRAVIS SCOTT', 'brands-travis-scott', 5),
    ('SUPREME', 'brands-supreme', 5),
    ('STONE ISLAND', 'brands-stone-island', 5),
    ('NIKE', 'brands-nike', 5),
    ('MEDICOM TOY', 'brands-medicom-toy', 5),
    ('LOUIS VUITTON', 'brands-louis-vuitton', 5),
    ('ADIDAS', 'brands-adidas', 5),
    ('CHROME HEARTS', 'brands-chrome-hearts', 5);

-- Субкатегории "Информация"
INSERT INTO "NavigationItems" ("Name", "Link", "ParentItem_Id")
VALUES
    ('Контакты', 'info-contacts', 6),
    ('Доставка', 'info-delivery', 6),
    ('Оплата', 'info-payment', 6),
    ('Возврат', 'info-returns', 6),
    ('FAQ', 'info-faq', 6),
    ('О нас', 'info-about', 6);

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

INSERT INTO "NMShop"."Orders" (
    "ClientFullName",
    "DeliveryAdress",
    "DeliveryType_Id",
    "PaymentType_Id",
    "OrderStatus_Id",
    "DeliveryRecipientFullName",
    "DeliveryRecipientPhone",
    "Comment",
    "ContactMethod_Id",
    "ContactValue",
    "PromoCode_Id",
    "EstimatedDeliveryDateRange"
)
VALUES
    (
        'Иван Иванов',
        'г. Москва, ул. Ленина, д. 1',
        1,
        1,
        1,
        'Иван Иванов',
        '89991234567',
        'Нет комментариев',
        1,
        'TrueSamyra',
        NULL,
        '15.10.24 - 16.10.24'
    ),
    (
        'Анна Смирнова',
        'г. Санкт-Петербург, ул. Садовая, д. 2',
        2,
        2,
        2,
        'Анна Смирнова',
        '89991112233',
        'Доставить вечером',
        2,
        'Sharp_Attilov',
        NULL,
        '17.10.24 - 18.10.24'
    ),
    (
        'Сергей Петров',
        'г. Новосибирск, пр. Ленина, д. 10',
        2,
        2,
        2,
        'Сергей Петров',
        '89998887766',
        'Позвонить заранее',
        2,
        'Sharp_Attilov',
        NULL,
        '19.10.24 - 20.10.24'
    );

-- Вставка данных в таблицу TextSizes
INSERT INTO "TextSizes" ("Value")
VALUES
    ('h1'),
    ('h2'),
    ('h3'),
    ('h4'),
    ('h5'),
    ('h6'),
    ('subtitle1'),
    ('subtitle2'),
    ('body1'),
    ('body2'),
    ('input'),
    ('button'),
    ('caption'),
    ('overline');

-- Вставка родительского топика ProductPageReferenceInfo
INSERT INTO "ReferenceTopics" ("Code", "Name")
VALUES
    ('product_page_reference_info', 'Product Page Reference Info')
RETURNING "Id";

-- Получение идентификатора родительского топика ProductPageReferenceInfo
WITH parent_topic AS (
    SELECT "Id" FROM "ReferenceTopics" WHERE "Code" = 'product_page_reference_info'
)

-- Вставка данных в таблицу ReferenceTopics с указанием родительского топика
INSERT INTO "ReferenceTopics" ("Code", "Name", "ParentTopic_Id")
VALUES
    ('product_delivery_methods', 'Способы доставки', (SELECT "Id" FROM parent_topic)),
    ('product_payment_methods', 'Способы оплаты', (SELECT "Id" FROM parent_topic)),
    ('product_faq', 'FAQ', (SELECT "Id" FROM parent_topic))
RETURNING "Id";

-- Присвоение идентификаторов для ReferenceTopics
WITH reference_topics AS (
    SELECT "Id", "Code" FROM "ReferenceTopics"
)

-- Вставка данных в таблицу ReferenceContent
INSERT INTO "ReferenceContent" ("Topic_Id", "TextSize_Id", "Content", "IsBold")
VALUES
    -- product_delivery_methods
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h4'), 'Мы работаем над тем, чтобы наши клиенты были самыми счастливыми, поэтому стараемся доставлять заказы максимально быстро и комфортно для Вас.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h5'), 'Сейчас доступны следующие варианты доставки:', true),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- доставка в любой магазин NIKITA EFREMOV.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- доставка домой или в офис курьерской службой.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), 'Доставка осуществляется для заказов, включающих не более 5-ти позиций.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- Доставка курьерской службой производится в рамках сроков обозначенных после оформления заказа.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- Доставка в любой регион России и стран СНГ осуществляется курьерской службой.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h5'), 'Также Вы можете оформить индивидуальный заказ понравившегося Вам товара по 100% предоплате.', true),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- Срок доставки: 7-14 рабочих дней.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_delivery_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- После оформления заявки на индивидуальный заказ, персональный менеджер свяжется с Вами.', false),

    -- product_payment_methods
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_payment_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h4'), 'Мы принимаем оплату банковскими картами:', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_payment_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- Visa, Mastercard, МИР.', true),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_payment_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- Долями и Яндекс.Сплит.', true),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_payment_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), '- СБП и др.', true),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_payment_methods'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h5'), 'Мы также принимаем наличные в магазинах и можем создать электронную ссылку для оплаты.', false),

    -- product_faq
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_faq'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h5'), '- Вы реализуете оригинальные товары?', true),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_faq'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), 'Мы продаем только новые и 100% оригинальные товары.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_faq'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h5'), '- Почему цена зависит от размера?', true),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_faq'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), 'Стоимость зависит от спроса на определенные размеры.', false),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_faq'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h5'), '- Какие размеры указаны на сайте?', true),
    ((SELECT "Id" FROM reference_topics WHERE "Code" = 'product_faq'), (SELECT "Id" FROM "TextSizes" WHERE "Value" = 'h6'), 'Вся обувь на сайте представлена в US размерах с таблицей соответствия.', false);


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








