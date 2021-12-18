CREATE DEFINER=`root`@`localhost` PROCEDURE `GetClients`()
BEGIN
	SELECT * FROM clients
    ORDER BY `clients`.`client_id`;
END