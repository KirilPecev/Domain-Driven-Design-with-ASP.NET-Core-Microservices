echo "CarRentalSystem.Admin"

docker build -t kpecev/carrentalsystem-admin-client -f .\CarRentalSystem.Admin\Dockerfile .

docker push kpecev/carrentalsystem-admin-client

echo "Done with CarRentalSystem.Admin"

::-------------------------------------------------------------

echo "CarRentalSystem.Dealers"

docker build -t kpecev/carrentalsystem-dealers-service -f .\CarRentalSystem.Dealers\Dockerfile .

docker push kpecev/carrentalsystem-dealers-service

echo "Done with CarRentalSystem.Dealers"

::----------------------------------------------------------------

echo "CarRentalSystem.Identity"

docker build -t kpecev/carrentalsystem-identity-service -f .\CarRentalSystem.Identity\Dockerfile .

docker push kpecev/carrentalsystem-identity-service

echo "Done with CarRentalSystem.Identity"

::----------------------------------------------------------------

echo "CarRentalSystem.Statistics"

docker build -t kpecev/carrentalsystem-statistics-service -f .\CarRentalSystem.Statistics\Dockerfile .

docker push kpecev/carrentalsystem-statistics-service

echo "Done with CarRentalSystem.Statistics"

::----------------------------------------------------------------

echo "CarRentalSystem.Notifications"

docker build -t kpecev/carrentalsystem-notifications-service -f .\CarRentalSystem.Notifications\Dockerfile .

docker push kpecev/carrentalsystem-notifications-service

echo "Done with CarRentalSystem.Notifications"

::----------------------------------------------------------------

echo "CarRentalSystem.Watchdog"

docker build -t kpecev/carrentalsystem-watchdog-service -f .\CarRentalSystem.Watchdog\Dockerfile .

docker push kpecev/carrentalsystem-watchdog-service

echo "Done with CarRentalSystem.Watchdog"