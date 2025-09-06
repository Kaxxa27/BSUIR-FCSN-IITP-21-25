const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const TicketController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM ticket');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM ticket WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("ticket not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { reservation_date, price, flight_id, seat_id, passport_id } = req.body;

        try {
            const query = `
                INSERT INTO ticket(reservation_date, price, flight_id, seat_id, passport_id)
                VALUES (?, ?, ?, ?, ?)
            `;

            await pool.query(query, [reservation_date, price, flight_id, seat_id, passport_id]);

            res.status(201).json({ message: 'ticket created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const {reservation_date, price, flight_id, seat_id, passport_id } = req.body;

        try {
            const query = `
                UPDATE ticket
                SET reservation_date = ?, price = ?, flight_id = ?, seat_id = ?, passport_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [reservation_date, price, flight_id, seat_id, passport_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("ticket not found"));
            }

            res.status(200).json({ message: 'ticket updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM ticket WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("ticket not found"));
            }

            res.status(200).json({ message: 'ticket deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = TicketController;