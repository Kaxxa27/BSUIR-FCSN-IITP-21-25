const ParkingSpot = require('../models/parkingSpot.model');

// Get all parking spots
exports.getAllParkingSpots = async (req, res) => {
    try {
        const parkingSpots = await ParkingSpot.find();
        res.status(200).json(parkingSpots);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Get ParkingSpot by Id 
exports.getParkingSpotById = async (req, res) => {
    try {
        const parkingSpot = await ParkingSpot.findById(req.params.id);
        if (!parkingSpot) {
            return res.status(404).json({ message: 'Parking spot not found' });
        }
        res.status(200).json(parkingSpot);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Create new ParkingSpot
exports.createParkingSpot = async (req, res) => {
    try {
        // Creating new ParkingSpot
        const newParkingSpot = new ParkingSpot(req.body);
        
        await newParkingSpot.save();
        res.status(201).json(newParkingSpot);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Update ParkingSpot
exports.updateParkingSpot = async (req, res) => {
    try {
        const parkingSpot = await ParkingSpot.findByIdAndUpdate(
            req.params.id,
            req.body,
            { new: true, runValidators: true }
        );

        if (!parkingSpot) {
            return res.status(404).json({ message: 'Parking spot not found' });
        }

        res.status(200).json(parkingSpot);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Remove ParkingSpot
exports.deleteParkingSpot = async (req, res) => {
    try {
        const parkingSpot = await ParkingSpot.findByIdAndDelete(req.params.id);

        if (!parkingSpot) {
            return res.status(404).json({ message: 'Parking spot not found' });
        }

        res.status(200).json({ message: 'Parking spot deleted successfully' });
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};
