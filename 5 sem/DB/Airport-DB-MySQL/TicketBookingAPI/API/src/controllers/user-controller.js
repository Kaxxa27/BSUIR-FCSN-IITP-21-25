const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const UserController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM user');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM user WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("User not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { username, password, email, is_active, is_admin, is_staff} = req.body;
        try {

            await pool.query(
                'CALL CreateUser(?, ?, ?, ?, ?, ?)', 
                [ username, password, email, is_active, is_admin, is_staff]
            );

            res.status(201).json({ message: 'User created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const userID = req.params.id;
        const { username, password, email, is_active, is_admin, is_staff } = req.body;
        
        try {

            const [result] = await pool.query(`CALL UpdateUserInfo(?, ?, ?, ?, ?, ?, ?);`, [userID, username, password, email, is_active, is_admin, is_staff]);

            if (!result.affectedRows) {
                return next(ApiError.NotFoundError("User not found"));
            }

            res.status(200).json({ message: 'User updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM user WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFoundError("User not found"));
            }

            res.status(200).json({ message: 'User deleted successfully' });
        } catch (e) {
            next(e);
        }
    }
};

module.exports = UserController;