CREATE DEFINER=`root`@`localhost` PROCEDURE `GetSit_places`()
BEGIN
	SELECT * FROM sit_places
	ORDER BY sit_places.`sit_place_id`;
END