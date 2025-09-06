const Account = require('../models/account.model');

// Get all accounts
exports.getAllAccounts = async (req, res) => {
    try {
        const accounts = await Account.find();
        res.status(200).json(accounts);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Get Account by Id 
exports.getAccountById = async (req, res) => {
    try {
        const account = await Account.findById(req.params.id);
        if (!account) {
            return res.status(404).json({ message: 'Account not found' });
        }
        res.status(200).json(account);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Create new Account
exports.createAccount = async (req, res) => {
    try {
        // Creating new Account
        const newAccount = new Account(req.body);
        
        await newAccount.save();
        res.status(201).json(newAccount);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Update Account
exports.updateAccount = async (req, res) => {
    try {
        const account = await Account.findByIdAndUpdate(
            req.params.id,
            req.body,
            { new: true, runValidators: true }
        );

        if (!account) {
            return res.status(404).json({ message: 'Account not found' });
        }

        res.status(200).json(account);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Remove Account
exports.deleteAccount = async (req, res) => {
    try {
        const account = await Account.findByIdAndDelete(req.params.id);

        if (!account) {
            return res.status(404).json({ message: 'Account not found' });
        }

        res.status(200).json({ message: 'Account deleted successfully' });
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};
