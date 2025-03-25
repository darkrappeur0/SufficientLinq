#include <iostream>
#include <vector>
#include <cstdlib>   
#include <ctime>     


template <typename T>
class RandomizedList {
private:
    std::vector<T> stuff;

public:
    
    RandomizedList() { srand(time(nullptr)); }

    T uniform(T a, T b){
        return (rand()%(b-a) + a );  //rand is giving a random number between 0 and 1
    }
    
    void add(T element) {
        if (rand() % 2 == 0){
            stuff.insert(stuff.begin(), element);  // Add at beginning
        }
        else{
            stuff.push_back(element);  // Add at end
        }
    }

    // Returns an element randomly chosen within the given index range
    T get(int index) {
        int randomIndex = rand() % (index + 1);
        return stuff[randomIndex];
    }

    // Checks if the list is empty
    bool isEmpty() const {
        return stuff.empty();
    }

    // Prints the list for debugging
    void printAll() const {
        std::cout << "Stuff: [";
        for (size_t i = 0; i < stuff.size(); i++) {
            std::cout << stuff[i];
            if (i != stuff.size() - 1){ std::cout << ", ";}
        }
        std::cout << "]" << std::endl;
    }
};



int main() {
    RandomizedList<int> randomList;

    // Adding elements
    for(int i=0;i<30;i++){
        randomList.add(randomList.uniform(0,1000));
    }

    randomList.printAll(); 

    int r = randomList.uniform(0,29);
    std::cout << "Random element from index range 0-"<< r << " :"<< randomList.get(2) << std::endl;

    // Checking if empty
    std::cout << "Is list empty? " << (randomList.isEmpty() ? "Yes" : "No") << std::endl;

    return 0;
}