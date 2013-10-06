-- phpMyAdmin SQL Dump
-- version 3.5.2.2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Oct 05, 2013 at 05:43 AM
-- Server version: 5.5.27
-- PHP Version: 5.4.7

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `susdulu`
--

-- --------------------------------------------------------

--
-- Table structure for table `airport`
--

CREATE TABLE IF NOT EXISTS `airport` (
  `ID` char(3) NOT NULL,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `airport`
--

INSERT INTO `airport` (`ID`, `name`) VALUES
('BDO', 'Bandung'),
('BPN', 'Balikpapan'),
('BTJ', 'Banda Aceh'),
('CGK', 'Jakarta'),
('DPS', 'Denpasar'),
('JOG', 'Yogyakarta'),
('LOP', 'Lombok'),
('MDC', 'Manado'),
('MES', 'Medan'),
('PDG', 'Padang'),
('PKU', 'Pekanbaru'),
('PLM', 'Palembang'),
('SOC', 'Solo'),
('SRG', 'Semarang'),
('SUB', 'Surabaya'),
('UPG', 'Makassar');

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `flight`
--

INSERT INTO `flight` (`ID`, `ID_plane`, `flight_number`, `departure_date`, `departure_time`, `arrival_date`, `arrival_time`, `origin`, `destination`, `distance`) VALUES
(1, 1, 'ABC1234', '2013-10-10', '07:15:00', '2013-10-10', '09:00:00', 'BDO', 'MES', 3000),
(2, 1, 'DEF4567', '2013-10-10', '12:00:00', '2013-10-10', '13:45:00', 'BDO', 'MES', 3000),
(3, 1, 'GHI1234', '2013-10-17', '07:15:00', '2013-10-17', '09:00:00', 'MES', 'BDO', 3000),
(4, 1, 'JKL5678', '2013-10-17', '12:00:00', '2013-10-17', '13:45:00', 'MES', 'BDO', 3000);

-- --------------------------------------------------------

--
-- Table structure for table `plane`
--

CREATE TABLE IF NOT EXISTS `plane` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `manufacturer` varchar(6) NOT NULL,
  `model` varchar(10) NOT NULL,
  `baggage_capacity` int(11) NOT NULL,
  `passenger_capacity` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `plane`
--

INSERT INTO `plane` (`ID`, `manufacturer`, `model`, `baggage_capacity`, `passenger_capacity`) VALUES
(1, 'Boeing', '737-900ER', 40400, 177),
(2, 'Airbus', 'A321-200', 25300, 185),
(3, 'Boeing', '747-8F', 227750, 467),
(4, 'Airbus', 'A380-800', 283200, 525);

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
  `address` varchar(200) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `gender` enum('MALE','FEMALE','') DEFAULT NULL,
  `city` varchar(100) DEFAULT NULL,
  `province` varchar(100) DEFAULT NULL,
  `postcode` varchar(10) DEFAULT NULL,
  `total_miles` int(11) DEFAULT NULL,
  `current_miles` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`ID`, `email`, `password`, `first_name`, `middle_name`, `last_name`, `address`, `phone`, `gender`, `city`, `province`, `postcode`, `total_miles`, `current_miles`) VALUES
(1, 'susdulu@informatika.org', '1000:ge9eiy1SeY8xsVX12joRl5etbNJh8rup:l8MjEoS2rNF2TvtIqxhaTHbtJWQaJqkD', 'Sus', NULL, 'Dulu', 'Jalan Ganesha 10', '14045', 'MALE', 'Bandung', 'Jawa Barat', '40132', 0, 0);

-- --------------------------------------------------------

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
