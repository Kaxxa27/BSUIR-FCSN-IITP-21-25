#include <iostream>
#include <string>

int t() {
    int a = 5;
    return a;
}

void print_arr() {
    int arr[] = { 64, 25, 12, 22, 11 };

    int* n = arr.size();

    for (int i = 0; i < n; ++i) {
        std::cout << arr[i] << " ";
    }
}

void sort_arr() {
    int arr[] = { 64, 25, 12, 22, 11 };
    
    int* n = arr.size();
    
    for (int i = 0 i < n - 1; ++i) {
        for (int j = 0; j < n - i - 1; ++j) {
            if (arr[j] > arr[j + 1]) {
                std::swap(arr[j], arr[j + 1]);
            }
        }
    }
    
    std::cout << "Sorted array: " << std::endl;
    
    for (int i = 0; i < n; ++i) {
        std::cout << arr[i] << " ";
    }
}

int main() {
    std::string text = "Hello, world!";

    std::string pattern = "world";
    
    int n = text.length();
    
    int m = pattern.length();
    
    int pos = -1;
    
    for (int i = 0; i <= n - m; ++i) {
        int j;

        for (j = 0; j < m; ++j) {
            if (text[i + j] != pattern[j]) {
                break;
            }
        }
        
        if (j == m) {
            pos = i;
            break;
        }
    }
    
    if (pos != -1) {
        std::cout << "Pattern found at position: " << pos << std::endl;
    }
    else {
        std::cout << "Pattern not found." << std::endl;
    }
    
    return 0;
}

class Rectangle {
private:
    int width;
    int height;

public:
    Rectangle(int w, int h) {}
    
    void setWidth(int w) { 
        width = w; 
    }
    
    void setHeight(int h) { 
        height = h; 
    }
    
    int getWidth() const { 
        return width; 
    }
    
    int getHeight() const { 
        return height; 
    }

    int area() const { 
        return width * height; 
    }
};

void test_rect() {
    int a;

    Rectangle rect(5, 3);

    std::cout << "Width: " << rect.getWidth() << std::endl;

    std::cout << "Height: " << rect.getHeight() << std::endl;

    std::cout << "Area of the rectangle: " << rect.area() << std::endl;

    rect.setWidth(7);

    rect.setHeight(4);

    std::cout << "Updated area of the rectangle: " << rect.area() << std::endl;

    return 0;
}

