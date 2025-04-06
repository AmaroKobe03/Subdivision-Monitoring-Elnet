-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 07, 2025 at 12:18 AM
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
CREATE DATABASE IF NOT EXISTS `neighborly` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `neighborly`;

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE `department` (
  `department_id` varchar(25) NOT NULL,
  `department_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`department_id`, `department_name`) VALUES
('DEPT101', 'HOUSEKEEPING'),
('DEPT102', 'MAINTENANCE'),
('DEPT103', 'SECURIT');

-- --------------------------------------------------------

--
-- Table structure for table `facilities`
--

CREATE TABLE `facilities` (
  `facility_id` varchar(50) NOT NULL,
  `facility_name` varchar(100) NOT NULL,
  `facility_category` enum('Sports','Recreation','Events') DEFAULT NULL,
  `facility_description` text DEFAULT NULL,
  `facilitiy_img` varchar(255) DEFAULT NULL,
  `service_fee_per_hour` decimal(10,2) NOT NULL,
  `facility_guidlines` text DEFAULT NULL,
  `facililty_aminities` text DEFAULT NULL,
  `typeof_status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `facilities`
--

INSERT INTO `facilities` (`facility_id`, `facility_name`, `facility_category`, `facility_description`, `facilitiy_img`, `service_fee_per_hour`, `facility_guidlines`, `facililty_aminities`, `typeof_status`) VALUES
('FAC-0001', 'Olympic Swimming Pool', 'Sports', 'An Olympic-sized swimming pool with 10 lanes for competitions and training.', 'images/swimming_pool.jpg', 50.00, 'No diving in shallow areas. Please follow lifeguard instructions.', 'Shower, Lockers, Lifeguard on duty', 1);

--
-- Triggers `facilities`
--
DELIMITER $$
CREATE TRIGGER `after_delete_facility` AFTER DELETE ON `facilities` FOR EACH ROW BEGIN
    DECLARE facility_count INT;

    -- Count remaining users for the deleted type_of_user
    SELECT COUNT(*) INTO facility_count
    FROM facilities;
    
    -- Reset the ID generator if no users remain for that role
    IF facility_count = 0 THEN
        UPDATE id_generator
        SET last_used_id = 0
        WHERE type_of_id = 4;
    END IF;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `before_insert_facilities` BEFORE INSERT ON `facilities` FOR EACH ROW BEGIN 
	DECLARE new_id VARCHAR(50);
    
    CALL generate_id (4, new_id);
    
    SET NEW.facility_id = new_id;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `facility_operating_hours`
--

CREATE TABLE `facility_operating_hours` (
  `facility_id` varchar(50) NOT NULL,
  `day_of_week` enum('Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday') NOT NULL,
  `opening_time` time NOT NULL,
  `closing_time` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `facility_operating_hours`
--

INSERT INTO `facility_operating_hours` (`facility_id`, `day_of_week`, `opening_time`, `closing_time`) VALUES
('FAC-0001', 'Sunday', '10:00:00', '18:00:00'),
('FAC-0001', 'Friday', '08:00:00', '20:00:00'),
('FAC-0001', 'Saturday', '10:00:00', '22:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `id_generator`
--

CREATE TABLE `id_generator` (
  `type_of_id` int(11) NOT NULL,
  `id_name` varchar(10) DEFAULT NULL,
  `last_used_id` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `id_generator`
--

INSERT INTO `id_generator` (`type_of_id`, `id_name`, `last_used_id`) VALUES
(1, 'Admin', 1),
(2, 'Resident', 3),
(3, 'Staff', 1),
(4, 'Facility', 1),
(5, 'Request', 6);

-- --------------------------------------------------------

--
-- Table structure for table `job_titles`
--

CREATE TABLE `job_titles` (
  `job_title_id` varchar(25) NOT NULL,
  `service_id` int(11) NOT NULL,
  `job_title_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `job_titles`
--

INSERT INTO `job_titles` (`job_title_id`, `service_id`, `job_title_name`) VALUES
('JOB101', 1, 'House Keeper'),
('JOB102', 2, 'Plumber'),
('JOB103', 3, 'Electrician'),
('JOB104', 4, 'Contractor'),
('JOB105', 5, 'Exterminator ');

-- --------------------------------------------------------

--
-- Table structure for table `posts`
--

CREATE TABLE `posts` (
  `post_id` varchar(50) NOT NULL,
  `user_id` varchar(50) NOT NULL,
  `post_date` date NOT NULL DEFAULT current_timestamp(),
  `post_text` varchar(500) NOT NULL,
  `post_media` varchar(100) NOT NULL,
  `likes` int(11) NOT NULL,
  `comments` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `posts`
--

INSERT INTO `posts` (`post_id`, `user_id`, `post_date`, `post_text`, `post_media`, `likes`, `comments`) VALUES
('2611947b-0068-4a91-a240-cecf8eabb49c', 'ADM-0001', '2025-04-07', 'Gusto ko nlng maging hotdog!', '', 0, 0),
('93682160-1009-48ba-8006-b8409d77339b', 'ADM-0001', '2025-04-07', 'wowow!', '', 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `requests`
--

CREATE TABLE `requests` (
  `request_id` varchar(50) NOT NULL,
  `requested_by` varchar(50) NOT NULL,
  `approved_by` varchar(50) DEFAULT NULL,
  `rtype_id` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT current_timestamp(),
  `typeof_status` int(11) NOT NULL DEFAULT 4
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `requests`
--

INSERT INTO `requests` (`request_id`, `requested_by`, `approved_by`, `rtype_id`, `created_at`, `typeof_status`) VALUES
('REQ-0001', 'RES-0001', 'ADM-0001', 1, '2025-04-02 14:36:25', 3),
('REQ-0002', 'RES-0001', NULL, 1, '2025-04-02 14:41:39', 4),
('REQ-0003', 'RES-0001', NULL, 1, '2025-04-02 14:46:57', 4),
('REQ-0004', 'RES-0001', NULL, 1, '2025-04-03 03:56:17', 4),
('REQ-0005', 'RES-0001', NULL, 1, '2025-04-06 11:17:24', 4),
('REQ-0006', 'RES-0001', NULL, 1, '2025-04-06 11:34:03', 4);

--
-- Triggers `requests`
--
DELIMITER $$
CREATE TRIGGER `after_delete_request` AFTER DELETE ON `requests` FOR EACH ROW BEGIN
    DECLARE request_count INT;

    -- Count remaining users for the deleted type_of_user
    SELECT COUNT(*) INTO request_count
    FROM requests;
    
    -- Reset the ID generator if no users remain for that role
    IF request_count = 0 THEN
        UPDATE id_generator
        SET last_used_id = 0
        WHERE type_of_id = 5; -- Request
    END IF;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `before_insert_request` BEFORE INSERT ON `requests` FOR EACH ROW BEGIN 
	DECLARE new_id VARCHAR(50);
    
    CALL generate_id (5, new_id);
    
    SET NEW.request_id = new_id;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `request_type`
--

CREATE TABLE `request_type` (
  `rtype_id` int(11) NOT NULL,
  `request_type` enum('Service','Visitor','Reservation') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `request_type`
--

INSERT INTO `request_type` (`rtype_id`, `request_type`) VALUES
(1, 'Service'),
(2, 'Visitor'),
(3, 'Reservation');

-- --------------------------------------------------------

--
-- Table structure for table `reservation_request_details`
--

CREATE TABLE `reservation_request_details` (
  `request_id` varchar(50) NOT NULL,
  `facility_id` varchar(50) NOT NULL,
  `reservation_date` date NOT NULL,
  `time_in` time NOT NULL,
  `time_out` time DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `residents`
--

CREATE TABLE `residents` (
  `resident_id` varchar(50) NOT NULL,
  `household_size` int(11) DEFAULT 1,
  `vehicle_count` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `residents`
--

INSERT INTO `residents` (`resident_id`, `household_size`, `vehicle_count`) VALUES
('RES-0001', 2, 1),
('RES-0002', 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `services`
--

CREATE TABLE `services` (
  `service_id` int(11) NOT NULL,
  `department_id` varchar(25) NOT NULL,
  `service_name` varchar(50) NOT NULL,
  `service_fee` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `services`
--

INSERT INTO `services` (`service_id`, `department_id`, `service_name`, `service_fee`) VALUES
(1, 'DEPT101', 'Housekeeping', 500.00),
(2, 'DEPT102', 'Plumbing', 500.00),
(3, 'DEPT102', 'Electrical', 700.00),
(4, 'DEPT102', 'Structural Repairs', 700.00),
(5, 'DEPT102', 'Pest Control', 500.00);

-- --------------------------------------------------------

--
-- Table structure for table `service_request`
--

CREATE TABLE `service_request` (
  `request_id` varchar(11) NOT NULL,
  `user_id` varchar(11) NOT NULL,
  `request_date` date NOT NULL,
  `request_time` varchar(11) NOT NULL,
  `request_creation` datetime NOT NULL DEFAULT current_timestamp(),
  `request_closed` datetime NOT NULL,
  `assigned_staff` varchar(11) NOT NULL,
  `request_status` varchar(15) NOT NULL,
  `request_type` varchar(15) NOT NULL,
  `request_subject` varchar(100) NOT NULL,
  `request_description` varchar(500) NOT NULL,
  `request_attachment` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `service_request`
--

INSERT INTO `service_request` (`request_id`, `user_id`, `request_date`, `request_time`, `request_creation`, `request_closed`, `assigned_staff`, `request_status`, `request_type`, `request_subject`, `request_description`, `request_attachment`) VALUES
('REQ-0001', 'RES-0003', '2025-04-11', '17:58:00', '2025-04-07 05:56:36', '0000-00-00 00:00:00', '', '', 'Maintenance', 'aaaa', 'aaaaa', ''),
('REQ-0002', 'RES-0003', '2025-04-11', '18:16', '2025-04-07 06:14:52', '0000-00-00 00:00:00', '', '', 'Maintenance', 'aaaadaas', 'adsadas', ''),
('REQ-0003', 'RES-0003', '2025-04-07', '06:17', '2025-04-07 06:17:34', '0000-00-00 00:00:00', '', '', 'Maintenance', 'Sleep Request', 'Ganahan nako matug HUHUHU', '');

-- --------------------------------------------------------

--
-- Table structure for table `service_request_details`
--

CREATE TABLE `service_request_details` (
  `request_id` varchar(50) NOT NULL,
  `service_id` int(11) DEFAULT NULL,
  `request_subject` text DEFAULT NULL,
  `date_schedule` date DEFAULT NULL,
  `time_schedule` time DEFAULT NULL,
  `request_description` text DEFAULT NULL,
  `attachments` varchar(255) DEFAULT NULL,
  `completed_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `service_request_details`
--

INSERT INTO `service_request_details` (`request_id`, `service_id`, `request_subject`, `date_schedule`, `time_schedule`, `request_description`, `attachments`, `completed_at`) VALUES
('REQ-0001', 1, 'Plumbing Issue', '2025-04-05', '10:00:00', 'Leaking pipe in the kitchen', 'leak_photo.jpg', NULL),
('REQ-0002', 2, 'Electrical Fix', '2025-04-06', '14:30:00', 'Power outlet not working', 'outlet_issue.png', NULL),
('REQ-0003', 3, 'General Cleaning', '2025-04-07', '09:00:00', 'Deep cleaning for the apartment', 'cleaning_request.docx', NULL),
('REQ-0004', 2, 'Light Replacement', '2025-04-09', '16:45:00', 'Replace LED lights in the hallway', NULL, NULL),
('REQ-0005', 1, 'Plumbing Issue', '2025-04-05', '10:00:00', 'Leaking pipe in the kitchen', 'leak_photo.jpg', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `staff`
--

CREATE TABLE `staff` (
  `staff_id` varchar(50) NOT NULL,
  `job_title_id` varchar(25) NOT NULL,
  `date_hired` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `staff`
--

INSERT INTO `staff` (`staff_id`, `job_title_id`, `date_hired`) VALUES
('STF-0001', 'JOB001', '2025-01-10');

-- --------------------------------------------------------

--
-- Table structure for table `staff_schedule`
--

CREATE TABLE `staff_schedule` (
  `staff_id` varchar(50) NOT NULL,
  `day_of_week` enum('Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday') NOT NULL,
  `start_time` time NOT NULL,
  `end_time` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `status`
--

CREATE TABLE `status` (
  `status_id` int(11) NOT NULL,
  `typeof_status` enum('Available','Unvailable','Approved','Pending','Cancelled','In Progress','Completed') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `status`
--

INSERT INTO `status` (`status_id`, `typeof_status`) VALUES
(1, 'Available'),
(2, 'Unvailable'),
(3, 'Approved'),
(4, 'Pending'),
(5, 'Cancelled'),
(6, 'In Progress'),
(7, 'Available'),
(8, 'Unvailable'),
(9, 'Approved'),
(10, 'Pending'),
(11, 'Cancelled'),
(12, 'In Progress'),
(13, 'Completed');

-- --------------------------------------------------------

--
-- Table structure for table `type_of_users`
--

CREATE TABLE `type_of_users` (
  `role_id` int(11) NOT NULL,
  `role_name` enum('Admin','Homeowner','Staff') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `type_of_users`
--

INSERT INTO `type_of_users` (`role_id`, `role_name`) VALUES
(1, 'Admin'),
(2, 'Homeowner'),
(3, 'Staff');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` varchar(50) DEFAULT NULL,
  `first_name` varchar(50) NOT NULL,
  `middle_name` varchar(50) DEFAULT NULL,
  `last_name` varchar(50) NOT NULL,
  `date_of_birth` date DEFAULT NULL,
  `gender` enum('Male','Female') DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `phone` char(13) DEFAULT NULL,
  `address` varchar(100) NOT NULL,
  `profile_picture` varchar(255) DEFAULT NULL,
  `type_of_user` int(11) NOT NULL,
  `created_at` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `first_name`, `middle_name`, `last_name`, `date_of_birth`, `gender`, `email`, `phone`, `address`, `profile_picture`, `type_of_user`, `created_at`) VALUES
('RES-0001', 'Emma', 'Lopez', 'Gonzalez', '1997-12-30', 'Female', 'emma.lopez@example.com', '0956789012', 'Cedar Lane', NULL, 2, '2025-04-02 03:01:09'),
('ADM-0001', 'Bryl', 'Brown', 'Gorgonio', '2003-03-11', 'Male', 'bryl@gmail.com', '0934567890', 'Pine Avenue', NULL, 1, '2025-04-02 03:01:32'),
('STF-0001', 'John', 'Doe', 'Smith', '1990-05-10', 'Male', 'john.doe@example.com', '0912345678', 'Elm Street', NULL, 3, '2025-04-02 03:02:21'),
('RES-0002', 'Sophia', 'Garcia', 'Rodriguez', '2000-03-25', 'Female', 'sophia.garcia@example.com', '0934567890', 'Elm Street', NULL, 2, '2025-04-02 03:02:29'),
('RES-0003', 'Kobe', 'A', 'Amaro', '2025-04-07', 'Male', 'kobe@gmail.com', '12345678901', 'Block #1 UC St.', '', 2, '2025-04-06 20:33:44');

--
-- Triggers `users`
--
DELIMITER $$
CREATE TRIGGER `after_delete_user` AFTER DELETE ON `users` FOR EACH ROW BEGIN
    DECLARE user_count INT;

    -- Count remaining users for the deleted type_of_user
    SELECT COUNT(*) INTO user_count
    FROM users
    WHERE type_of_user = OLD.type_of_user;

    -- Reset the ID generator if no users remain for that role
    IF user_count = 0 THEN
        UPDATE id_generator
        SET last_used_id = 0
        WHERE type_of_user = OLD.type_of_user;
    END IF;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `before_insert_user` BEFORE INSERT ON `users` FOR EACH ROW BEGIN
    DECLARE new_user_id VARCHAR(50);
    
    -- Call stored procedure to generate user_id
    CALL generate_id(NEW.type_of_user, new_user_id);
    
    -- Assign generated user_id to new record
    SET NEW.user_id = new_user_id;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `user_accounts`
--

CREATE TABLE `user_accounts` (
  `id` int(11) NOT NULL,
  `user_id` varchar(50) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `user_accounts`
--

INSERT INTO `user_accounts` (`id`, `user_id`, `username`, `password`) VALUES
(1, 'ADM-0001', 'bryl@gmail.com', '123'),
(2, 'RES-0003', 'kobe@gmail.com', '123');

-- --------------------------------------------------------

--
-- Table structure for table `_admin`
--

CREATE TABLE `_admin` (
  `admin_id` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `_admin`
--

INSERT INTO `_admin` (`admin_id`) VALUES
('ADM-0001');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`department_id`);

--
-- Indexes for table `facilities`
--
ALTER TABLE `facilities`
  ADD PRIMARY KEY (`facility_id`),
  ADD KEY `typeof_status` (`typeof_status`);

--
-- Indexes for table `facility_operating_hours`
--
ALTER TABLE `facility_operating_hours`
  ADD PRIMARY KEY (`facility_id`,`day_of_week`);

--
-- Indexes for table `id_generator`
--
ALTER TABLE `id_generator`
  ADD PRIMARY KEY (`type_of_id`);

--
-- Indexes for table `job_titles`
--
ALTER TABLE `job_titles`
  ADD PRIMARY KEY (`job_title_id`),
  ADD KEY `service_id` (`service_id`);

--
-- Indexes for table `posts`
--
ALTER TABLE `posts`
  ADD PRIMARY KEY (`post_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `requests`
--
ALTER TABLE `requests`
  ADD PRIMARY KEY (`request_id`),
  ADD KEY `approved_by` (`approved_by`),
  ADD KEY `typeof_status` (`typeof_status`),
  ADD KEY `rtype_id` (`rtype_id`),
  ADD KEY `requested_by` (`requested_by`);

--
-- Indexes for table `request_type`
--
ALTER TABLE `request_type`
  ADD PRIMARY KEY (`rtype_id`);

--
-- Indexes for table `reservation_request_details`
--
ALTER TABLE `reservation_request_details`
  ADD PRIMARY KEY (`request_id`),
  ADD KEY `facility_id` (`facility_id`);

--
-- Indexes for table `residents`
--
ALTER TABLE `residents`
  ADD PRIMARY KEY (`resident_id`);

--
-- Indexes for table `services`
--
ALTER TABLE `services`
  ADD PRIMARY KEY (`service_id`),
  ADD KEY `department_id` (`department_id`);

--
-- Indexes for table `service_request`
--
ALTER TABLE `service_request`
  ADD PRIMARY KEY (`request_id`);

--
-- Indexes for table `service_request_details`
--
ALTER TABLE `service_request_details`
  ADD PRIMARY KEY (`request_id`),
  ADD KEY `service_id` (`service_id`);

--
-- Indexes for table `staff`
--
ALTER TABLE `staff`
  ADD PRIMARY KEY (`staff_id`);

--
-- Indexes for table `staff_schedule`
--
ALTER TABLE `staff_schedule`
  ADD PRIMARY KEY (`staff_id`);

--
-- Indexes for table `status`
--
ALTER TABLE `status`
  ADD PRIMARY KEY (`status_id`);

--
-- Indexes for table `type_of_users`
--
ALTER TABLE `type_of_users`
  ADD PRIMARY KEY (`role_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD UNIQUE KEY `user_id` (`user_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `type_of_user` (`type_of_user`);

--
-- Indexes for table `user_accounts`
--
ALTER TABLE `user_accounts`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `_admin`
--
ALTER TABLE `_admin`
  ADD PRIMARY KEY (`admin_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `request_type`
--
ALTER TABLE `request_type`
  MODIFY `rtype_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `services`
--
ALTER TABLE `services`
  MODIFY `service_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `status`
--
ALTER TABLE `status`
  MODIFY `status_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `type_of_users`
--
ALTER TABLE `type_of_users`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `user_accounts`
--
ALTER TABLE `user_accounts`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `facilities`
--
ALTER TABLE `facilities`
  ADD CONSTRAINT `facilities_ibfk_1` FOREIGN KEY (`typeof_status`) REFERENCES `status` (`status_id`);

--
-- Constraints for table `facility_operating_hours`
--
ALTER TABLE `facility_operating_hours`
  ADD CONSTRAINT `facility_operating_hours_ibfk_1` FOREIGN KEY (`facility_id`) REFERENCES `facilities` (`facility_id`) ON DELETE CASCADE;

--
-- Constraints for table `job_titles`
--
ALTER TABLE `job_titles`
  ADD CONSTRAINT `job_titles_ibfk_1` FOREIGN KEY (`service_id`) REFERENCES `services` (`service_id`) ON DELETE CASCADE;

--
-- Constraints for table `posts`
--
ALTER TABLE `posts`
  ADD CONSTRAINT `posts_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `requests`
--
ALTER TABLE `requests`
  ADD CONSTRAINT `requests_ibfk_2` FOREIGN KEY (`approved_by`) REFERENCES `_admin` (`admin_id`),
  ADD CONSTRAINT `requests_ibfk_3` FOREIGN KEY (`typeof_status`) REFERENCES `status` (`status_id`),
  ADD CONSTRAINT `requests_ibfk_4` FOREIGN KEY (`rtype_id`) REFERENCES `request_type` (`rtype_id`),
  ADD CONSTRAINT `requests_ibfk_5` FOREIGN KEY (`requested_by`) REFERENCES `residents` (`resident_id`) ON DELETE CASCADE;

--
-- Constraints for table `reservation_request_details`
--
ALTER TABLE `reservation_request_details`
  ADD CONSTRAINT `reservation_request_details_ibfk_1` FOREIGN KEY (`request_id`) REFERENCES `requests` (`request_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `reservation_request_details_ibfk_2` FOREIGN KEY (`facility_id`) REFERENCES `facilities` (`facility_id`);

--
-- Constraints for table `residents`
--
ALTER TABLE `residents`
  ADD CONSTRAINT `residents_ibfk_1` FOREIGN KEY (`resident_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `services`
--
ALTER TABLE `services`
  ADD CONSTRAINT `services_ibfk_1` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`) ON DELETE CASCADE;

--
-- Constraints for table `service_request_details`
--
ALTER TABLE `service_request_details`
  ADD CONSTRAINT `service_request_details_ibfk_1` FOREIGN KEY (`request_id`) REFERENCES `requests` (`request_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `service_request_details_ibfk_2` FOREIGN KEY (`service_id`) REFERENCES `services` (`service_id`);

--
-- Constraints for table `staff`
--
ALTER TABLE `staff`
  ADD CONSTRAINT `staff_ibfk_1` FOREIGN KEY (`staff_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `staff_schedule`
--
ALTER TABLE `staff_schedule`
  ADD CONSTRAINT `staff_schedule_ibfk_1` FOREIGN KEY (`staff_id`) REFERENCES `staff` (`staff_id`) ON DELETE CASCADE;

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`type_of_user`) REFERENCES `type_of_users` (`role_id`);

--
-- Constraints for table `user_accounts`
--
ALTER TABLE `user_accounts`
  ADD CONSTRAINT `user_accounts_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `_admin`
--
ALTER TABLE `_admin`
  ADD CONSTRAINT `_admin_ibfk_1` FOREIGN KEY (`admin_id`) REFERENCES `users` (`user_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
