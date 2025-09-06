const express = require('express');
const router = express.Router();
const parkingSpotController = require('../controllers/parkingspot.controller');

// Routes for parking spots
router.get('/', parkingSpotController.getAllParkingSpots);
router.post('/', parkingSpotController.createParkingSpot);
router.get('/:id', parkingSpotController.getParkingSpotById);
router.put('/:id', parkingSpotController.updateParkingSpot);
router.delete('/:id', parkingSpotController.deleteParkingSpot);

module.exports = router;
