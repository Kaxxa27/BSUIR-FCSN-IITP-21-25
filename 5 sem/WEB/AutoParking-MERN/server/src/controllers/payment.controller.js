const Payment = require('../models/payment.model');

// Get all payments
exports.getAllPayments = async (req, res) => {
    try {
        const payments = await Payment.find();
        res.status(200).json(payments);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Get Payment by Id 
exports.getPaymentById = async (req, res) => {
    try {
        const payment = await Payment.findById(req.params.id);
        if (!payment) {
            return res.status(404).json({ message: 'Payment not found' });
        }
        res.status(200).json(payment);
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Create new Payment
exports.createPayment = async (req, res) => {
    try {
        // Creating new Payment
        const newPayment = new Payment(req.body);
        
        await newPayment.save();
        res.status(201).json(newPayment);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Update Payment
exports.updatePayment = async (req, res) => {
    try {
        const payment = await Payment.findByIdAndUpdate(
            req.params.id,
            req.body,
            { new: true, runValidators: true }
        );

        if (!payment) {
            return res.status(404).json({ message: 'Payment not found' });
        }

        res.status(200).json(payment);
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message
        });
    }
};

// Remove Payment
exports.deletePayment = async (req, res) => {
    try {
        const payment = await Payment.findByIdAndDelete(req.params.id);

        if (!payment) {
            return res.status(404).json({ message: 'Payment not found' });
        }

        res.status(200).json({ message: 'Payment deleted successfully' });
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message
        });
    }
};
