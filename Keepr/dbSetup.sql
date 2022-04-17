CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id int NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  img TEXT NOT NULL,
  views int NOT NULL,
  kept int NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  description TEXT NOT NULL,
  name TEXT NOT NULL,
  isPrivate TINYINT DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  keepId INT NOT NULL,
  vaultId INT NOT NULL,
  creatorId VARCHAR(255),
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
DROP TABLE vaultKeeps;
SELECT
  *
FROM
  vaultKeeps
WHERE
  vaultKeeps.vaultId = 38;
SELECT
  k.*,
  a.*,
  vk.id AS VaultKeepId
FROM
  vaultKeeps vk
  JOIN keeps k ON k.id = vk.keepId
  JOIN accounts a ON a.id = k.creatorId
WHERE
  vk.vaultId = 38;