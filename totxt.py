import os

# --- КОНФИГУРАЦИЯ ---

# Расширения файлов, которые нужно включить в итоговый TXT
INCLUDED_EXTENSIONS = (
    '.cs',
    '.xaml',
    '.csproj',
    '.config',
    '.json',
    '.settings',
    '.txt',
    '.md' # Если есть файлы README
)

# Папки, которые следует игнорировать (служебные, временные, бинарные)
EXCLUDED_DIRS = (
    'bin',
    'obj',
    '.git',
    '.vs',
    'packages',
    'node_modules',
    'TestResults'
)

OUTPUT_FILENAME = "wpf_project_dump.txt"

# --- ОСНОВНАЯ ЛОГИКА СКРИПТА ---

def process_project():
    """
    Обходит текущую директорию, собирает исходные файлы
    и записывает их содержимое в один TXT-файл.
    """
    root_dir = os.getcwd()
    file_count = 0
    
    print(f"Начало обработки проекта в: {root_dir}")
    print(f"Результат будет записан в файл: {OUTPUT_FILENAME}")

    try:
        with open(OUTPUT_FILENAME, 'w', encoding='utf-8') as outfile:
            
            # Рекурсивный обход директорий
            for root, dirs, files in os.walk(root_dir):
                
                # Исключение служебных папок (модифицируем список dirs на месте)
                dirs[:] = [d for d in dirs if d not in EXCLUDED_DIRS]
                
                for file_name in files:
                    # Проверка расширения файла
                    if file_name.endswith(INCLUDED_EXTENSIONS):
                        
                        full_path = os.path.join(root, file_name)
                        # Получаем путь относительно корня проекта
                        relative_path = os.path.relpath(full_path, root_dir)

                        try:
                            # Чтение файла (используем UTF-8, стандартную кодировку для C#)
                            with open(full_path, 'r', encoding='utf-8') as infile:
                                content = infile.read()
                            
                            # --- Форматирование для AI ---
                            # Добавляем разделитель и имя файла, чтобы AI знал, что анализирует
                            outfile.write(f"==========================================================\n")
                            outfile.write(f"--- FILE PATH: {relative_path} ---\n")
                            outfile.write(f"==========================================================\n\n")
                            
                            outfile.write(content)
                            
                            outfile.write(f"\n\n--- END OF {relative_path} ---\n\n")
                            
                            file_count += 1

                        except UnicodeDecodeError:
                            print(f"[ПРЕДУПРЕЖДЕНИЕ] Пропущен файл из-за проблем с кодировкой: {relative_path}")
                        except Exception as e:
                            print(f"[ОШИБКА] Не удалось прочитать файл {relative_path}: {e}")

        print("\n---------------------------------------------------")
        print(f"Процесс завершен. Обработано файлов: {file_count}")
        print(f"Файл для AI создан: {os.path.abspath(OUTPUT_FILENAME)}")
        print("---------------------------------------------------")

    except Exception as e:
        print(f"Критическая ошибка при записи выходного файла: {e}")

if __name__ == "__main__":
    process_project()