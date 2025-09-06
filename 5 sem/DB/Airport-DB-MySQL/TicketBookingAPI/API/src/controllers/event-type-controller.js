const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const EventTypeController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM event_type');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM event_type WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("event_type not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { name } = req.body;

        try {
            const query = `
                INSERT INTO event_type(name)
                VALUES (?)
            `;

            await pool.query(query, [name]);

            res.status(201).json({ message: 'event_type created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { name } = req.body;

        try {
            const query = `
                UPDATE event_type
                SET name = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [name, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("event_type not found"));
            }

            res.status(200).json({ message: 'event_type updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM event_type WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("event_type not found"));
            }

            res.status(200).json({ message: 'event_type deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = EventTypeController;