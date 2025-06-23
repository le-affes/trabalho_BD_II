import { useEffect, useState } from 'react';
import { Container } from '../../../styles/components/pages/Home/Famous';
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

export default function Famous() {
  const [coords, setCoords] = useState<{ latitude: number; longitude: number } | null>(null);

  const [restaurants, setRestaurants] = useState<IRestaurant[]>([]);
  useEffect(() => {
    navigator.geolocation.getCurrentPosition(position => {
      const { latitude, longitude } = position.coords;
      setCoords({ latitude, longitude });
    });
  }, []);

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
  }, []);

  return (
    <Container className="scroll-box">
      <header>
        <h4>Famosos</h4>
        <a href="#">Ver mais</a>
      </header>
      <div role="list" className="scroll-box__wrapper">
        {restaurants.length === 0 ? (
          <RestaurantPlaceHolder repeatCount={5} isFamousContainer />
        ) : (
          restaurants.map(restaurant => (
            <Restaurant
              key={restaurant.title}
              restaurantData={restaurant}
              isFamousContainer
            />
          ))
        )}
      </div>
    </Container>
  );
}
