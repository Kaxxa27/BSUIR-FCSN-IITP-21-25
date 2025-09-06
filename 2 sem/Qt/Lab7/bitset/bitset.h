#ifndef BITSET_H
#define BITSET_H

#include <QString>

#define bit (8 * sizeof(int))

/*

~ -> ~(01) = 10
& -> 0011 & 0101 = 0001
| -> 0011 | 0101 = 0111
^(XOR) -> 0011 ^ 0101 = 0110

*/
template<size_t Bits>
class bitset {

private:

    int bits[Bits / 32 + 1];//bits[0] -> 32 bits

    bool get_bit(size_t bit_num) {//get value of num_bit (T | F)

        return ((bits[bit_num / bit] & (1U << (bit_num % bit)))); // 1U -> unsigned value 1
    }

    void _reset(size_t bit_num) { //reset one bit -> false

        bits[bit_num / bit] &= ~(1U << (bit_num % bit));
    }

    void _set(size_t bit_num) {//set one bit -> true

        bits[bit_num / bit] |= (1U << (bit_num % bit));
    }

public:

    bitset() {//default full false

        for (size_t i = 0; i < Bits; ++i)
            reset(i);

    }

    void set(size_t bit_num) { // FULL 1 -> 0000 | 1000 = 1000 ...

        if (bit_num >= Bits)
            return;

        bit_num = Bits - bit_num;

        --bit_num;

        bits[bit_num / bit] |= (1U << (bit_num % bit));

    }

    void reset(size_t bit_num) { // FULL 0 -> 0000 & ~(1000) -> 0000 & 0111 = 0000

        if (bit_num >= Bits)
            return;

        bit_num = Bits - bit_num;

        --bit_num;

        bits[bit_num / bit] &= ~(1U << (bit_num % bit));

    }

    void reset() {

        for (size_t i = 0; i < Bits; ++i)
            reset(i);

    }

    bool test(size_t bit_num) {// отличие от [] только в throw std::out_of_range

        if (bit_num >= Bits)
            return false;

        bit_num = Bits - bit_num;

        --bit_num;

        return (bits[bit_num / bit] & (1U << (bit_num % bit)));

    }

    void flip(size_t bit_num) {//only one bit on pos // ~

        if (bit_num >= Bits)
            return;

        bit_num = Bits - bit_num;

        --bit_num;

        bits[bit_num / bit] ^= (1U << (bit_num % bit));

    }

    void flip() {//FUll bits // ~

        for (size_t i = 0; i < Bits; ++i) {

            if (get_bit(i))
                _reset(i);
            else
                _set(i);
        }

    }

    bool operator[] (size_t bit_num) {//doesn't throw exceptions// get_bit == []

        if (bit_num >= Bits)
            return false;

        bit_num = Bits - bit_num;

        --bit_num;

        return ((bits[bit_num / bit] & (1U << (bit_num % bit))));
    }

    size_t size() noexcept {

        return Bits;

    }

    size_t count() {//count of true bits

        size_t counter = 0;

        for (size_t i = 0; i < Bits; ++i)
            if (get_bit(i))
                ++counter;

        return counter;

    }

    QString to_string() {

        QString s_bits;

        for (size_t i = 0; i < Bits; i++) {

            s_bits.push_back(get_bit(i) ? '1' : '0');

        }

        return s_bits;

    }

    unsigned to_ulong() {//ulong num

        unsigned num = 0;

        for (size_t i = 0; i < Bits; ++i)
            num += get_bit(i) * pow(2, Bits - 1 - i);

        return num;

    }

    bool any() {// хотя бы один бит -> true

        for (size_t i = 0; i <= Bits; ++i)
            if (get_bit(i))
                return true;

        return false;

    }

    bool none() {//all bits is false

        return !any();

    }

    bool all() {//all bits is true

        return count() == size();
    }

};

#endif // BITSET_H
