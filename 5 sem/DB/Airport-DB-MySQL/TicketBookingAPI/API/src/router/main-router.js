const router = require('express').Router();

router.use('/auth', require('./auth-router'));
router.use('/user', require('./user-router'));
router.use('/passport', require('./passport-router'));
router.use('/gender', require('./gender-router'));
router.use('/nationality', require('./nationality-router'));
router.use('/country', require('./country-router'));
router.use('/city', require('./city-router'));
router.use('/event', require('./event-router'));
router.use('/event-type', require('./event-type-router'));
router.use('/ticket', require('./ticket-router'));
router.use('/flight', require('./flight-router'));
router.use('/schedule', require('./schedule-router'));
router.use('/baggage', require('./baggage-router'));
router.use('/airline', require('./airline-router'));
router.use('/seat', require('./seat-router'));
router.use('/plane', require('./plane-router'));
router.use('/airport', require('./airport-router'));
router.use('/seatclass', require('./seatclass-router'));
router.use('/scripts', require('./scripts-router'));

module.exports = router;