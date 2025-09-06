const mongoose = require('mongoose');

// ParkingSpot
const parkingSpotSchema = new mongoose.Schema({
  number: {
    type: Number,
    required: true,
    unique: true,
  },
  price: {
    type: Number,
    required: true,
  },
  is_busy: {
    type: Boolean,
    default: false,
  },
  cars: [{
    type: mongoose.Schema.Types.ObjectId,
    ref: 'Car',
  }],
  user: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'User',
  },
  date_of_rent: {
    type: Date,
  },
});
  
const ParkingSpot = mongoose.model('ParkingSpot', parkingSpotSchema);

module.exports = ParkingSpot;

  