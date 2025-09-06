const User = require('../models/user.model');

// Get all users
exports.getAllUsers = async (req, res) => {
    try {
        const users = await User.find();
        res.status(200).json(users);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Get User by Id 
exports.getUserById = async (req, res) => {
    try {
        const user = await User.findById(req.params.id);
        if(!user){
            return res.status(404).json({meassage: 'User was not found' });
        }
        res.status(200).json(user);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};



// Create new User
exports.createUser = async (req, res) => {

    try {
        // Creating new User
        const newUser = new User(req.body);
        
        await newUser.save();
        res.status(201).json(newUser);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
}

// Update User
exports.updateUser = async (req, res) => {
    try {
        const user = await User.findByIdAndUpdate(
            req.params.id,
            req.body,
            { new: true, runValidators: true }
        );

        if (!user) {
            return res.status(404).json({ message: 'User not found' });
        }

        res.status(200).json(user);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Remove User
exports.deleteUser = async (req, res) => {
    try {
        const user = await User.findByIdAndDelete(req.params.id);

        if (!user) {
            return res.status(404).json({ message: 'User not found' });
        }

        res.status(200).json({ message: 'User deleted successfully' });
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

