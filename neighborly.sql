-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 25, 2025 at 08:50 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `neighborly`
--
CREATE DATABASE IF NOT EXISTS `neighborly` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `neighborly`;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` varchar(50) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `middle_name` varchar(50) DEFAULT NULL,
  `last_name` varchar(50) NOT NULL,
  `date_of_birth` date DEFAULT NULL,
  `gender` enum('Male','Female') DEFAULT NULL,
  `phone_number` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `valid_id` varchar(50) NOT NULL,
  `address` varchar(255) NOT NULL,
  `role` enum('Homeowner','Admin','Housekeeping','Maintenance','Security') DEFAULT NULL,
  `profile_picture` varchar(500) DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `first_name`, `middle_name`, `last_name`, `date_of_birth`, `gender`, `phone_number`, `email`, `valid_id`, `address`, `role`, `profile_picture`, `created_at`) VALUES
('1', 'Christine', 'Aguhar', 'Alesna', '2025-02-28', 'Male', '09454280932', 'christine@gmail.com', '', 'Cebu City', 'Housekeeping', '', '2025-03-12 04:51:33'),
('M001', 'Kobe', '', 'Amaro', '2003-08-11', 'Male', '09216800240', 'kobe@gmail.com', '20119154', 'cebu city', 'Admin', 'profile.png', '2025-03-12 03:48:21'),
('M002', 'Bryl', 'Mejeca', 'Gorgonio', '2003-04-17', 'Male', '09454280932', 'bryl@gmail.com', '20949194', 'cebu city', 'Homeowner', 'profile.png', '2025-03-12 03:48:21');

-- --------------------------------------------------------

--
-- Table structure for table `user_accounts`
--

CREATE TABLE `user_accounts` (
  `user_id` varchar(50) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user_accounts`
--

INSERT INTO `user_accounts` (`user_id`, `username`, `password`) VALUES
('1', 'christine', '123'),
('M001', 'Kobe', '123'),
('M002', 'Bryl', '123');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `valid_id` (`valid_id`);

--
-- Indexes for table `user_accounts`
--
ALTER TABLE `user_accounts`
  ADD PRIMARY KEY (`user_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
