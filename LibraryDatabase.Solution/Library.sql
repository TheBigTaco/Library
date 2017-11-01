-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'authors'
--
-- ---

DROP TABLE IF EXISTS `authors`;

CREATE TABLE `authors` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'books'
--
-- ---

DROP TABLE IF EXISTS `books`;

CREATE TABLE `books` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `title` VARCHAR(255) NULL DEFAULT NULL,
  `amount` INTEGER NULL DEFAULT NULL,
  `amount_checkedout` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'authors_books'
--
-- ---

DROP TABLE IF EXISTS `authors_books`;

CREATE TABLE `authors_books` (
  `author_id` INTEGER NULL DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL
);

-- ---
-- Table 'patrons'
--
-- ---

DROP TABLE IF EXISTS `patrons`;

CREATE TABLE `patrons` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Table 'checkouts'
--
-- ---

DROP TABLE IF EXISTS `checkouts`;

CREATE TABLE `checkouts` (
  `book_id` INTEGER NULL DEFAULT NULL,
  `patron_id` INTEGER NULL DEFAULT NULL,
  `due_date` DATE NULL DEFAULT NULL,
  `returned` SMALLINT NULL DEFAULT NULL
);

-- ---
-- Foreign Keys
-- ---

ALTER TABLE `authors_books` ADD FOREIGN KEY (author_id) REFERENCES `authors` (`id`);
ALTER TABLE `authors_books` ADD FOREIGN KEY (book_id) REFERENCES `books` (`id`);
ALTER TABLE `checkouts` ADD FOREIGN KEY (book_id) REFERENCES `books` (`id`);
ALTER TABLE `checkouts` ADD FOREIGN KEY (patron_id) REFERENCES `patrons` (`id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `authors` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `authors_books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `patrons` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `checkouts` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `authors` (`id`,`name`) VALUES
-- ('','');
-- INSERT INTO `books` (`id`,`title`,`amount`,`amount_checkedout`) VALUES
-- ('','','','');
-- INSERT INTO `authors_books` (`author_id`,`book_id`) VALUES
-- ('','');
-- INSERT INTO `patrons` (`id`,`name`) VALUES
-- ('','');
-- INSERT INTO `checkouts` (`book_id`,`patron_id`,`due_date`,`returned`) VALUES
-- ('','','','');
