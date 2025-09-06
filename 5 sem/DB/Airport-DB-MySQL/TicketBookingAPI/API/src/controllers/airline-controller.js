const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const AirlineController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM airline');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM airline WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("airline not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { name, description, country_id} = req.body;

        try {
            const query = `
                INSERT INTO airline(name, description, country_id)
                VALUES (?, ?, ?)
            `;

            await pool.query(query, [name, description, country_id]);

            res.status(201).json({ message: 'airline created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const {name, description, country_id } = req.body;

        try {
            const query = `
                UPDATE airline
                SET name = ?, description = ?, country_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [name, description, country_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("airline not found"));
            }

            res.status(200).json({ message: 'airline updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM airline WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("airline not found"));
            }

            res.status(200).json({ message: 'airline deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = AirlineController;