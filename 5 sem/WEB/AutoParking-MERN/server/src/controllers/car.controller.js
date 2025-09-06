const Car = require('../models/car.model');

// Get all cars
exports.getAllCars = async (req, res) => {
    try {
        const cars = await Car.find();
        res.status(200).json(cars);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Get Car by Id 
exports.getCarById = async (req, res) => {
    try {
        const car = await Car.findById(req.params.id);
        if (!car) {
            return res.status(404).json({ message: 'Car not found' });
        }
        res.status(200).json(car);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Create new Car
exports.createCar = async (req, res) => {
    try {
        // Creating new Car
        const newCar = new Car(req.body);
        
        await newCar.save();
        res.status(201).json(newCar);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Update Car
exports.updateCar = async (req, res) => {
    try {
        const car = await Car.findByIdAndUpdate(
            req.params.id,
            req.body,
            { new: true, runValidators: true }
        );

        if (!car) {
            return res.status(404).json({ message: 'Car not found' });
        }

        res.status(200).json(car);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Remove Car
exports.deleteCar = async (req, res) => {
    try {
        const car = await Car.findByIdAndDelete(req.params.id);

        if (!car) {
            return res.status(404).json({ message: 'Car not found' });
        }

        res.status(200).json({ message: 'Car deleted successfully' });
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};
