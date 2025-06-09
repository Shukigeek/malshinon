-- creat new database
CREATE DATABASE IF NOT EXISTS malshinon;

-- use malshinon databasre
USE Malshinon;

-- creat people table with column
CREATE TABLE IF NOT EXISTS People(
    id INT AUTO_INCREMENT,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    secret_code VARCHAR(50) UNIQUE,
    type ENUM('reporter','target','both','potential_agent'),
    num_reports INT DEFAULT(0),
    num_mentions INT DEFAULT(0), 
    PRIMARY KEY(id)
);
-- creat intel reports table with columns
CREATE TABLE IF NOT EXISTS IntelReports(
    id INT AUTO_INCREMENT,
    reportr_id INT,
    target_id INT,
    text TEXT,
    timestamp DATETIME DEFAULT NOW(), 
    PRIMARY KEY(id),
    FOREIGN KEY(reportr_id) REFERENCES People(id),
    FOREIGN KEY(target_id) REFERENCES People(id)
);