-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'books'
--
-- ---

DROP TABLE IF EXISTS `books`;

CREATE TABLE `books` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `title` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

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
  `copies_id` INTEGER NULL DEFAULT NULL,
  `patrons_id` INTEGER NULL DEFAULT NULL,
  `checkout_date` DATE NULL DEFAULT NULL,
  `due_date` DATE NULL DEFAULT NULL
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
-- Table 'books_patrons'
--
-- ---

DROP TABLE IF EXISTS `books_patrons`;

CREATE TABLE `books_patrons` (
  `book_id` INTEGER NULL DEFAULT NULL,
  `patron_id` INTEGER NULL DEFAULT NULL
);

-- ---
-- Table 'copies'
--
-- ---

DROP TABLE IF EXISTS `copies`;

CREATE TABLE `copies` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `book_id` INTEGER NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Foreign Keys
-- ---

ALTER TABLE `checkouts` ADD FOREIGN KEY (copies_id) REFERENCES `copies` (`id`);
ALTER TABLE `checkouts` ADD FOREIGN KEY (patrons_id) REFERENCES `patrons` (`id`);
ALTER TABLE `authors_books` ADD FOREIGN KEY (author_id) REFERENCES `authors` (`id`);
ALTER TABLE `authors_books` ADD FOREIGN KEY (book_id) REFERENCES `books` (`id`);
ALTER TABLE `books_patrons` ADD FOREIGN KEY (book_id) REFERENCES `books` (`id`);
ALTER TABLE `books_patrons` ADD FOREIGN KEY (patron_id) REFERENCES `patrons` (`id`);
ALTER TABLE `copies` ADD FOREIGN KEY (book_id) REFERENCES `books` (`id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `authors` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `patrons` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `checkouts` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `authors_books` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `books_patrons` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `copies` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `books` (`id`,`title`) VALUES
-- ('','');
-- INSERT INTO `authors` (`id`,`name`) VALUES
-- ('','');
-- INSERT INTO `patrons` (`id`,`name`) VALUES
-- ('','');
-- INSERT INTO `checkouts` (`copies_id`,`patrons_id`,`checkout_date`,`due_date`) VALUES
-- ('','','','');
-- INSERT INTO `authors_books` (`author_id`,`book_id`) VALUES
-- ('','');
-- INSERT INTO `books_patrons` (`book_id`,`patron_id`) VALUES
-- ('','');
-- INSERT INTO `copies` (`id`,`book_id`) VALUES
-- ('','');
