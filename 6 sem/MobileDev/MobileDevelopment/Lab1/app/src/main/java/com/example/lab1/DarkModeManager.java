package com.example.lab1;

import android.view.View;
import android.widget.Switch;

import androidx.appcompat.app.AppCompatDelegate;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

public class DarkModeManager {
    private DatabaseReference themeRef;
    private Switch switcher;

    public DarkModeManager(Switch switcher) {
        this.switcher = switcher;
        // Получение ссылки на узел темы в Firebase
        themeRef = FirebaseDatabase.getInstance().getReference().child("themes").child("darkMode");

        // Получение значения темы из Firebase асинхронно
        themeRef.get().addOnCompleteListener(new OnCompleteListener<DataSnapshot>() {
            @Override
            public void onComplete(Task<DataSnapshot> task) {
                if (task.isSuccessful()) {
                    Boolean darkMode = task.getResult().getValue(Boolean.class);
                    if (darkMode != null && darkMode) {
                        switcher.setChecked(true);
                        AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES);
                    }
                }
            }
        });

        // Обработчик изменений в переключателе
        switcher.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                // Обновление значения в Firebase асинхронно
                themeRef.setValue(switcher.isChecked()).addOnCompleteListener(new OnCompleteListener<Void>() {
                    @Override
                    public void onComplete(Task<Void> task) {
                        if (!task.isSuccessful()) {
                            // Обработка ошибки записи данных
                        }
                    }
                });
                // Обновление темы приложения
                if (switcher.isChecked()) {
                    AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES);
                } else {
                    AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_NO);
                }
            }
        });
    }
}
