-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Nov 09, 2017 at 09:59 PM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cityexperiences`
--
CREATE DATABASE IF NOT EXISTS `cityexperiences` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `cityexperiences`;

-- --------------------------------------------------------

--
-- Table structure for table `bookings`
--

CREATE TABLE `bookings` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `experience_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `bookings`
--

INSERT INTO `bookings` (`id`, `user_id`, `experience_id`) VALUES
(1, 7, 25),
(2, 9, 26),
(3, 9, 28),
(4, 10, 25),
(5, 11, 26),
(6, 11, 30),
(7, 11, 30),
(8, 11, 30),
(9, 11, 30);

-- --------------------------------------------------------

--
-- Table structure for table `cities`
--

CREATE TABLE `cities` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `photo_link` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cities`
--

INSERT INTO `cities` (`id`, `name`, `photo_link`) VALUES
(1, 'Tokyo', 'http://www.thecamouflagecollective.org/wp-content/uploads/2017/03/tokyo-mud-bath-bar-mudbath0716-620x388.jpg'),
(2, 'Singapore', 'http://www.telegraph.co.uk/content/dam/Travel/Destinations/Asia/Singapore/Singapore%20lead-xlarge.jpg'),
(3, 'Paris', 'http://www.planetware.com/photos-large/F/france-paris-eiffel-tower.jpg'),
(4, 'London', 'https://realbusiness.co.uk/wp-content/uploads/2017/02/shutterstock_362072633.jpg'),
(5, 'New York', 'http://www.ahstatic.com/photos/2185_hodesti_00_p_1024x768.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `experiences`
--

CREATE TABLE `experiences` (
  `id` int(11) NOT NULL,
  `location_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `title` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `photo_link` varchar(255) NOT NULL,
  `price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `experiences`
--

INSERT INTO `experiences` (`id`, `location_id`, `user_id`, `title`, `description`, `photo_link`, `price`) VALUES
(1, 3, 1, 'Wine Tasting', 'A guided wine tasting tour ', 'https://www.louisville.com/sites/default/files/article_image/wine1.jpg', 30),
(3, 1, 6, 'Tokyo Disneyland', 'Disneyland', 'https://guiddoo.s3.amazonaws.com/IMAGE/saveblogfeatureimage_5a96f5e4-ff34-4f1e-8c24-420cfce52413.jpg', 66),
(4, 1, 6, 'Sushi Class', 'Learn How to make Sushi!', 'https://cache-graphicslib.viator.com/graphicslib/thumbs360x240/36359/SITours/tokyo-tsukiji-outer-market-walking-tour-and-rolled-sushi-class-in-tokyo-502408.jpg', 180),
(5, 1, 6, 'Tokyo Owl Cafe', 'Get up close and personal with these big eyed beauties~', 'http://tokyostory.net/wp-content/uploads/owl3.jpg', 22),
(6, 1, 6, 'Tokyo National Museum', 'Learn about the rich history of Tokyo\'s past', 'https://www.japan-guide.com/g9/3054_tokyo_11.jpg', 6),
(7, 1, 6, 'Private City Tour', 'Take a tour of Tokyo and see the sights that you don\'t get to see on regular tours', 'http://cdn.cnn.com/cnnnext/dam/assets/170606110126-tokyo-skyline-super-tease.jpg', 210),
(8, 5, 6, 'Helicopter Tour of NYC', 'Ride a Helicopter!', 'https://media-cdn.tripadvisor.com/media/photo-s/06/0a/8c/76/liberty-helicopter-tours.jpg', 233),
(9, 5, 6, 'Metropolitan Museum of Art', 'Head to the Metropolitan Museum of Art in the heart of Manhattan', 'https://media-cdn.tripadvisor.com/media/photo-s/04/22/1c/fc/metropolitan-museum-of.jpg', 40),
(13, 3, 6, 'Visit the Eiffel Tower', 'Travel up one of the 7 wonders of the world in Paris.', 'https://www.attractiontix.co.uk/images/photosGSD/the-eiffel-tower-tour/the-eiffel-tower-tour4.jpg', 30),
(14, 3, 6, 'Visit the Louvre', 'See the artwork of Renaissance masters! ', 'https://upload.wikimedia.org/wikipedia/commons/thumb/6/66/Louvre_Museum_Wikimedia_Commons.jpg/800px-Louvre_Museum_Wikimedia_Commons.jpg', 18),
(15, 3, 6, 'Tour the Notre Dame Cathedral', 'Second only to the Eiffel Tower, Notre Dame Cathedral (Cathedrale de Notre Dame de Paris) is one of Paris\' most iconic attractions, a marvel of medieval architecture that was immortalized in Victor Hugo\'s classic novel The Hunchback of Notre Dame. Today, ', 'https://cache-graphicslib.viator.com/graphicslib/page-images/360x240/100102_Paris_Notre%20Dame%20Cathedral%20(Cathedrale%20de%20Notre%20Dame%20de%20Paris)_d479-510.jpg', 21),
(16, 3, 6, 'Skip the Lines: Paris Catacombs Tour', 'Visit the Paris Underground tunnels filled with the remains of overflowing cemeteries. Fun for all ages!', 'http://nomadicalsabbatical.com/wp-content/uploads/2014/02/Paris-Catacombs-Long-Term-Travel1.jpg', 94),
(17, 5, 6, 'Top of the Rock VIP tour', 'Skip the lines as you climb the Rockafeller Center in New York City. ', 'https://d1v5vpeyrmf36z.cloudfront.net/media/CACHE/images/image-previews/landscape/BuyTix_1200x800/6c663e6a6c07836d57a90ebd712f7f02.jpg', 76),
(18, 5, 6, '9/11 Memorial Tour', '', 'https://www.nycgo.com/images/venues/191/9-11-memorial-03-marley-white__x_large.jpg', 35),
(19, 5, 6, 'Explore Central Park', 'Explore the beauty of Central Park', 'https://media.cntraveler.com/photos/58d5450f9033b024213c8e8e/master/w_480,c_limit/GettyImages-125756141.jpg', 0),
(20, 4, 6, 'London Eye cruise', 'Take a tour around the London Eye along the Thames River', 'https://d1wgio6yfhqlw1.cloudfront.net/sysimages/product/resized6/London_Eye_River_Cruise_4645_18363.jpg', 15),
(21, 4, 6, 'Tour the British Parliament and Westminster Abbey', 'Tours of Parliament and Westminster Abbey', 'https://19623-presscdn-pagely.netdna-ssl.com/wp-content/uploads/2014/05/parliament-feat.jpg', 100),
(22, 4, 6, 'Harry Potter Behind the Scenes Tour', 'Tour Warner Brothers\' Studios in London for a BTS look at the Harry Potter Stages', 'https://d1wgio6yfhqlw1.cloudfront.net/sysimages/product/resized6/The_Great_Hall_3865_21763.jpg', 96),
(23, 2, 6, 'Gardens by the Bay Tour', 'Gardens by the Bay is part of a strategy by the Singapore government to transform Singapore from a \"Garden City\" to a \"City in a Garden\". The stated aim is to raise the quality of life by enhancing greenery and flora in the city.', 'http://static.asiawebdirect.com/m/phuket/portals/www-singapore-com/homepage/attractions/gardens-by-the-bay/pagePropertiesImage/gardens-by-the-bay-singapore.jpg.jpg', 24),
(24, 4, 1, 'Personalised Photography Session', 'I will bring you to the most scenic spots in London for a personalised photoshoot, this package comes with 40 digital photos of you!', 'http://www.margaritakarenko.com/wp-content/uploads/2014/04/London_outdoor_fashion_portrait_photoshoot_Big-Ben_01.jpg', 150),
(25, 1, 2, 'Authentic Ramen Making Lesson', 'I will teach you how to make Authentic Japanese Ramen in my comfortable kitchen', 'https://i.ytimg.com/vi/qRSZEN6g8jY/maxresdefault.jpg', 50),
(26, 3, 7, 'Abstract Art Lesson', 'I am an artist and i will show you my gallery. We will have a lesson on abstract art and you get to paint on a canvas', 'http://hyperallergic.com/wp-content/uploads/2015/12/JohannaBarron_%C2%A9JamesRexroad_8009713_lr.jpg', 100),
(30, 4, 11, 'Zumba', 'ZUMBA® is a fusion of Latin and International music / dance themes that create a dynamic, exciting, and based on the principle that a workout should be \"FUN AND EASY TO DO.\" The routines feature aerobic/fitness interval training with a combination of fast', 'https://studiofitnessvictoria.com/wp-content/uploads/sites/20/2017/05/zumba_class.jpg', 80);

-- --------------------------------------------------------

--
-- Table structure for table `experiences_tags`
--

CREATE TABLE `experiences_tags` (
  `id` int(11) NOT NULL,
  `experience_id` int(11) NOT NULL,
  `tag_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `experiences_tags`
--

INSERT INTO `experiences_tags` (`id`, `experience_id`, `tag_id`) VALUES
(1, 3, 1),
(2, 3, 2),
(3, 3, 3),
(4, 4, 4),
(5, 4, 5),
(6, 4, 6),
(7, 4, 7),
(8, 4, 8),
(9, 5, 9),
(10, 5, 10),
(11, 5, 6),
(12, 5, 11),
(13, 5, 12),
(14, 6, 12),
(15, 6, 13),
(16, 6, 14),
(17, 6, 15),
(18, 6, 16),
(19, 7, 16),
(20, 7, 17),
(21, 7, 18),
(22, 7, 19),
(23, 8, 20),
(24, 8, 16),
(25, 8, 21),
(26, 8, 6),
(27, 9, 15),
(28, 9, 12),
(29, 9, 13),
(30, 9, 22),
(31, 9, 23),
(32, 10, 15),
(33, 10, 12),
(34, 10, 13),
(35, 10, 22),
(36, 10, 23),
(37, 11, 15),
(38, 11, 12),
(39, 11, 13),
(40, 11, 22),
(41, 11, 23),
(42, 12, 15),
(43, 12, 12),
(44, 12, 13),
(45, 12, 22),
(46, 12, 23),
(47, 13, 22),
(48, 13, 24),
(49, 13, 25),
(50, 13, 26),
(51, 14, 23),
(52, 14, 15),
(53, 14, 26),
(54, 15, 14),
(55, 15, 16),
(56, 15, 26),
(57, 16, 26),
(58, 16, 27),
(59, 16, 16),
(60, 16, 28),
(61, 17, 29),
(62, 17, 16),
(63, 17, 30),
(64, 17, 31),
(65, 18, 32),
(66, 18, 21),
(67, 18, 33),
(68, 19, 33),
(69, 19, 34),
(70, 19, 6),
(71, 20, 35),
(72, 20, 36),
(73, 20, 16),
(74, 20, 37),
(75, 20, 34),
(76, 21, 16),
(77, 21, 35),
(78, 21, 34),
(79, 22, 34),
(80, 22, 38),
(81, 22, 24),
(82, 23, 39),
(83, 23, 16),
(84, 23, 24),
(85, 23, 34),
(86, 23, 40),
(87, 23, 31),
(88, 24, 41),
(89, 24, 35),
(90, 24, 42),
(91, 24, 43),
(92, 25, 44),
(93, 25, 7),
(94, 25, 45),
(95, 25, 46),
(96, 25, 47),
(97, 26, 23),
(98, 26, 48),
(99, 26, 26),
(100, 26, 49),
(101, 26, 50),
(102, 26, 51),
(103, 27, 4),
(104, 28, 31),
(105, 29, 6),
(106, 29, 52),
(107, 29, 4),
(108, 30, 53),
(109, 30, 54);

-- --------------------------------------------------------

--
-- Table structure for table `tags`
--

CREATE TABLE `tags` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tags`
--

INSERT INTO `tags` (`id`, `name`) VALUES
(1, 'Kid-Friendly'),
(2, 'Disney'),
(3, 'Adult'),
(4, 'food'),
(5, 'sushi'),
(6, 'fun'),
(7, 'japan'),
(8, 'kawaii'),
(9, 'owl'),
(10, 'animal'),
(11, 'cute'),
(12, 'kid-friendly'),
(13, 'family'),
(14, 'historical'),
(15, 'museum'),
(16, 'tour'),
(17, 'private'),
(18, 'boujee'),
(19, 'upscale'),
(20, 'helicopter'),
(21, 'nyc'),
(22, 'history'),
(23, 'art'),
(24, 'sightseeing'),
(25, 'climb'),
(26, 'paris'),
(27, 'creepy'),
(28, 'night'),
(29, 'New-York'),
(30, 'adult'),
(31, ''),
(32, 'memorial'),
(33, 'new-york'),
(34, 'family-friendly'),
(35, 'London'),
(36, 'river'),
(37, 'boat'),
(38, 'famous'),
(39, 'singapore'),
(40, 'romance'),
(41, 'photoshoot'),
(42, 'england'),
(43, 'photo'),
(44, 'ramen'),
(45, 'cooking'),
(46, 'tokyo'),
(47, 'authentic'),
(48, 'painting'),
(49, 'artwork'),
(50, 'gallery'),
(51, 'lesson'),
(52, 'asia'),
(53, 'dance'),
(54, 'exercise');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `date_of_birth` varchar(255) NOT NULL,
  `country` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `phone` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `name`, `date_of_birth`, `country`, `email`, `phone`, `password`) VALUES
(1, 'Daniel Cheng', '1999-12-12', 'Malaysia', 'danielchengml@gmail.com', '2223334444', 'danielcheng'),
(2, 'Arthur Brown', '1966-02-23', 'United States', 'arthur@brown.com', '2221113345', 'arthurbrown'),
(3, 'Jerry Mouse', '2000-01-01', 'Tom Cat', 'jerry@runs.tom', '9119119111', 'jerrymouse'),
(4, 'Goofy Goof', '2002-02-22', 'Venezuela', 'goot.goot.com', '222222222', 'goofgoof'),
(5, 'Christopher Columbu', '1621-10-08', 'Spain', 'christopher@columbus.com', '2223334444', 'chrisc'),
(6, 'Gyles Batara', '1993-10-03', 'United States', 'gyles.batara@live.com', '2063991797', 'gylesb'),
(7, 'Vonda Gracie', '1970-02-01', 'France', 'vonda@vondagracie.com', '3334445555', 'vondagracie'),
(10, 'jesse', '1989-05-14', 'usa', '1@1.com', '214235234', '1');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bookings`
--
ALTER TABLE `bookings`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cities`
--
ALTER TABLE `cities`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `experiences`
--
ALTER TABLE `experiences`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `experiences_tags`
--
ALTER TABLE `experiences_tags`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tags`
--
ALTER TABLE `tags`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bookings`
--
ALTER TABLE `bookings`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `cities`
--
ALTER TABLE `cities`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `experiences`
--
ALTER TABLE `experiences`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;
--
-- AUTO_INCREMENT for table `experiences_tags`
--
ALTER TABLE `experiences_tags`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=110;
--
-- AUTO_INCREMENT for table `tags`
--
ALTER TABLE `tags`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=55;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
