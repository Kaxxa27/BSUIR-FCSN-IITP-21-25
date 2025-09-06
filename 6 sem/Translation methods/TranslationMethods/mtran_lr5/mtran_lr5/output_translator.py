
def main():
    numbers = []

    while True:
        num = int(input("Enter number(exit = 0): "))
        if num == 0:
            break
        numbers.append(num)

    print("Our numbers:")
    for n in numbers:
        print(n, end=" ")
    print()

if __name__ == "__main__":
    main()

	
