const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const PlaneController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM plane');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM plane WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("plane not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { model, capacity, registration_number, is_under_maintenance } = req.body;

        try {
            const query = `
                INSERT INTO plane(model, capacity, registration_number, is_under_maintenance)
                VALUES (?, ?, ?, ?)
            `;

            await pool.query(query, [model, capacity, registration_number, is_under_maintenance]);

            res.status(201).json({ message: 'plane created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { model, capacity, registration_number, is_under_maintenance } = req.body;

        try {
            const query = `
                UPDATE plane
                SET model = ?, capacity = ?, registration_number = ?, is_under_maintenance = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [model, capacity, registration_number, is_under_maintenance, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("plane not found"));
            }

            res.status(200).json({ message: 'plane updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM plane WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("plane not found"));
            }

            res.status(200).json({ message: 'plane deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = PlaneController;