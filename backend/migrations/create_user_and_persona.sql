CREATE TABLE `Users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `omnis` int DEFAULT '0',
  `guloks` boolean DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Personas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `name` varchar(255) NOT NULL,
  `games` int NOT NULL DEFAULT '0',
  `points` int NOT NULL DEFAULT '0',
  `victories` int NOT NULL DEFAULT '0',
  `visionary` int NOT NULL DEFAULT '0',
  `sweeper` int NOT NULL DEFAULT '0',
  `trader` int NOT NULL DEFAULT '0',
  `attacked` int NOT NULL DEFAULT '0',
  `credits` int NOT NULL DEFAULT '0',
  `tutorial` boolean DEFAULT '0',
  `species` varchar(15) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
