В качестве способа синхронизации потоков я использовал Semaphore. Этот инструмент позволяет предоставить доступ к объекту ограниченному числу потоков. 
В моем проекте семафорами являются стол(DiningTableSemaphore) и вилки(Forks).
Одновременно к приему пищи могут приступить не более половины философов, а одной вилкой может пользоваться только один философ.
В целом Semaphore задается по следующему принципу: Semaphore(initialCount , maximumCount).
initialCount - инициализируемое количество потоков, которым семафор может предоставить доступ к объекту, или текущий ресурс семафора, maximumCount - максимальное их количество.
Для предоставления потоку доступа используется команда Semaphore.WaitOne(), она блокирует поток, пока тот не получит доступ к объекту. Можно сказать потоки встают в очередь, чтобы получить разрешение от семафора на пользование объектом. Когда поток получает доступ, он уменьшает Count семафора на 1.
Для освобождения ресура используется команда Semaphore.Release(). Она увеличивает Count на 1, и возвращает значение Count. По сути, этой командой мы двигаем очередь потоков.
