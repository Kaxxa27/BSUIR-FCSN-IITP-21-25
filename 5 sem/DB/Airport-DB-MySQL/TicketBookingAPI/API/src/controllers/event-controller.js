const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const EventController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM event');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM event WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("event not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { name,datetime, description, user_id, event_type_id } = req.body;

        try {
            const query = `
                INSERT INTO event(name, datetime, description, user_id, event_type_id)
                VALUES (?, ?,?, ?,?)
            `;

            await pool.query(query, [name, datetime,description, user_id, event_type_id]);

            res.status(201).json({ message: 'event created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const {name,datetime,  description, user_id, event_type_id } = req.body;

        try {
            const query = `
                UPDATE event
                SET name = ?,datetime = ?, description = ?, user_id = ?, event_type_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [name, datetime, description, user_id, event_type_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("event not found"));
            }

            res.status(200).json({ message: 'event updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM event WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("event not found"));
            }

            res.status(200).json({ message: 'event deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = EventController;