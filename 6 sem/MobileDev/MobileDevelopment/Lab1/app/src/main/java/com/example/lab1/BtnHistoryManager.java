package com.example.lab1;

import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;

public class BtnHistoryManager {
    private DatabaseReference mDatabase;
    private DatabaseReference mResultsDatabase;
    public BtnHistoryManager() {
        mDatabase = FirebaseDatabase.getInstance().getReference().child("button_press_history");
        mResultsDatabase = FirebaseDatabase.getInstance().getReference().child("results");
    }

    public void saveButtonPress(String buttonText) {
        // Генерируем уникальный ключ для каждого элемента истории нажатия кнопок
        String key = mDatabase.push().getKey();

        // Сохраняем объект в базе данных
        mDatabase.child(key).setValue(buttonText);
    }
    public void saveResultPress(String result, String operation) {
        String key = mResultsDatabase.push().getKey();
        ResultPress res = new ResultPress(result, operation);

        // Сохраняем объект в базе данных
        mResultsDatabase.child(key).setValue(res);
    }

    // Метод для чтения последнего объекта из mResultsDatabase
    public void readLastResult(OnResultLoadedListener listener) {
        mResultsDatabase.limitToLast(1).addListenerForSingleValueEvent(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot dataSnapshot) {
                // Получаем последний элемент из базы данных
                for (DataSnapshot snapshot : dataSnapshot.getChildren()) {
                    ResultPress result = snapshot.getValue(ResultPress.class);
                    // Передаем результат через слушателя
                    if (listener != null) {
                        listener.onResultLoaded(result);
                    }
                }
            }

            @Override
            public void onCancelled(DatabaseError databaseError) {
                // Обработка ошибок
            }
        });
    }

    // Интерфейс слушателя для передачи результата обратно вызывающему коду
    public interface OnResultLoadedListener {
        void onResultLoaded(ResultPress result);
    }
}

