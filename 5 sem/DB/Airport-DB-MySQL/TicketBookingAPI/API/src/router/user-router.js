const router = require('express').Router();
const controller = require('../controllers/user-controller');
const authMiddleware = require('../middleware/auth-middleware');
const adminMiddleware = require('../middleware/admin-middleware');

router.get('/', controller.getAll);
router.get('/:id', controller.getById);
router.post('/', [authMiddleware, adminMiddleware], controller.create);
router.put('/:id', [authMiddleware, adminMiddleware], controller.update);
router.delete('/:id', [authMiddleware, adminMiddleware], controller.delete);

module.exports = router;