import { useEffect, useState } from 'react';

import { Container } from '../../../styles/components/pages/Home/SuggestedRestaurants';
import Restaurant from './Restaurant';

import api from '../../../services/api';
import RestaurantPlaceHolder from './placeholders/RestaurantPlaceholder';

interface IRestaurant {
  id: number;
  title: string;
  image_url: string;
  category: string;
  distance: number;
  start_time: number;
  end_time: number;
  rating: number;
}

export default function SuggestedRestaurants() {
  const [coords, setCoords] = useState<{ latitude: number; longitude: number } | null>(null);
  const [restaurants, setRestaurants] = useState<IRestaurant[]>([]);

  // Pegando a localização do usuário
  useEffect(() => {
    navigator.geolocation.getCurrentPosition(position => {
      const { latitude, longitude } = position.coords;
      setCoords({ latitude, longitude });
    });
  }, []);

  // Chamando a API depois de obter as coordenadas
  useEffect(() => {
    async function loadRestaurants() {
      if (coords) {
        const response = await api.get('restaurants', {
          params: {
            latitude: coords.latitude,
            longitude: coords.longitude,
          },
        });

        setRestaurants(response.data);
      }
    }

    loadRestaurants();
  }, [coords]); // Executa sempre que as coordenadas forem carregadas

  return (
    <Container>
      <div>
        <h4>Restaurantes e mercados</h4>

        <ul>
          {restaurants.length === 0 ? (
            <RestaurantPlaceHolder repeatCount={9} isFamousContainer={false} />
          ) : (
            restaurants.map(restaurant => (
              <li key={restaurant.title}>
                <Restaurant restaurantData={restaurant} />
              </li>
            ))
          )}
        </ul>
        <button type="button">Ver mais restaurantes e mercados</button>
      </div>
    </Container>
  );
}
