-- Delete all of the data
DELETE from reservation;
DELETE FROM site;
DELETE FROM campground;
DELETE FROM park;

-- Insert a fake park
INSERT INTO park (name, location, establish_date, area, visitors, description)
VALUES ('Peak District', 'UK', '1951-01-25', 555, 10000000, 'Despite its name, the landscape generally lacks sharp peaks, and is characterised mostly by rounded hills, plateaus, valleys, limestone gorges and gritstone escarpments ');

DECLARE @newParkId int = @@IDENTITY;

-- Insert a fake campground
INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) 
VALUES (@newParkId, 'Hayfield', 1, 12, 25.00);

DECLARE @newCampgroundId int = @@IDENTITY;

-- Insert a fake site
INSERT INTO site (site_number, campground_id) VALUES (1, @newCampgroundId);
INSERT INTO site (site_number, campground_id, utilities) VALUES (2, @newCampgroundId, 1);
INSERT INTO site (site_number, campground_id, accessible) VALUES (3, @newCampgroundId, 1);
INSERT INTO site (site_number, campground_id, accessible, utilities) VALUES (4, @newCampgroundId, 1, 1);

DECLARE @newSiteId int = @@IDENTITY;

-- Assign the fake reservation
INSERT INTO reservation (site_id, name, from_date, to_date) VALUES ( @newSiteId, 'McClung Family Reservation', GETDATE()-2, GETDATE()+2);

DECLARE @newReservationId int = @@IDENTITY;

-- Return the ids of the fake items
SELECT @newParkId as newParkId, @newCampgroundId as newCampgroundId, @newSiteId as newSiteId, @newReservationId as newReservationId
