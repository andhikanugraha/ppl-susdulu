-- phpMyAdmin SQL Dump
-- version 4.0.4.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Oct 03, 2013 at 02:17 PM
-- Server version: 5.5.32
-- PHP Version: 5.4.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `susdulubeneran`
--
CREATE DATABASE IF NOT EXISTS `susdulubeneran` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `susdulubeneran`;

-- --------------------------------------------------------

--
-- Table structure for table `airport`
--

CREATE TABLE IF NOT EXISTS `airport` (
  `ID` char(3) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `flight`
--

CREATE TABLE IF NOT EXISTS `flight` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ID_plane` int(11) NOT NULL,
  `flight_number` varchar(10) NOT NULL,
  `departure_date` date NOT NULL,
  `departure_time` time NOT NULL,
  `arrival_date` date NOT NULL,
  `arrival_time` time NOT NULL,
  `origin` char(3) NOT NULL,
  `destination` char(3) NOT NULL,
  `distance` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID_plane` (`ID_plane`,`origin`,`destination`),
  KEY `destination` (`destination`),
  KEY `origin` (`origin`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `plane`
--

CREATE TABLE IF NOT EXISTS `plane` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `model` varchar(10) NOT NULL,
  `baggage_capacity` int(11) NOT NULL,
  `passenger_capacity` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `ticket`
--

CREATE TABLE IF NOT EXISTS `ticket` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ID_user` int(11) NOT NULL,
  `ID_flight` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `middle_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `address` varchar(200) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `gender` enum('MALE','FEMALE','','') NOT NULL,
  `city` varchar(100) NOT NULL,
  `province` varchar(100) NOT NULL,
  `postcode` varchar(10) NOT NULL,
  `class` enum('FIRSTCLASS','BUSINESS','ECONOMY','') NOT NULL,
  `price` int(11) NOT NULL,
  `seat` varchar(10) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID_user` (`ID_user`,`ID_flight`),
  KEY `ID_flight` (`ID_flight`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `middle_name` varchar(50) DEFAULT NULL,
  `last_name` varchar(50) DEFAULT NULL,
  `address` varchar(200) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `gender` enum('MALE','FEMALE','','') NOT NULL,
  `city` varchar(100) NOT NULL,
  `province` varchar(100) NOT NULL,
  `postcode` varchar(10) NOT NULL,
  `total_miles` int(11) NOT NULL,
  `current_miles` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `flight`
--
ALTER TABLE `flight`
  ADD CONSTRAINT `flight_ibfk_1` FOREIGN KEY (`ID_plane`) REFERENCES `plane` (`ID`),
  ADD CONSTRAINT `flight_ibfk_2` FOREIGN KEY (`origin`) REFERENCES `airport` (`ID`),
  ADD CONSTRAINT `flight_ibfk_3` FOREIGN KEY (`destination`) REFERENCES `airport` (`ID`);

--
-- Constraints for table `ticket`
--
ALTER TABLE `ticket`
  ADD CONSTRAINT `ticket_ibfk_1` FOREIGN KEY (`ID_user`) REFERENCES `user` (`ID`),
  ADD CONSTRAINT `ticket_ibfk_2` FOREIGN KEY (`ID_flight`) REFERENCES `flight` (`ID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
