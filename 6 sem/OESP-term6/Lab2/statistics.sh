#!/bin/bash

# Check parametrs in cmd line
if [ $# -ne 2 ]; then
    echo "Usage: $0 <input_file> <output_file>"
    echo $help_info
    exit 1
fi

# Params 
input_file=$1
output_file=$2

# Check that file exist
if [ ! -f "$input_file" ]; then
    echo "File $input_file not found!"
    exit 1
fi

# Create outpet file, if doesn't exist 
> "$output_file"

awk -F- '{
    name = $1;
    count[name] += $2;
    total_price[name] += $2 * $3;
}
END {
    for (name in count) {
        avg_price = count[name] > 0 ? total_price[name] / count[name] : 0;
        printf("{\n    \"Name\": \"%s\",\n", name);
        printf("    \"Count\": %d,\n", count[name]);
        printf("    \"Average Price\": %.2f,\n", avg_price);
        printf("    \"Total Price\": %.2f\n}\n", total_price[name]);
    }
}' "$input_file" >> "$output_file"

echo "Result saved in $output_file"