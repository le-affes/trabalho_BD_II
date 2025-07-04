import { GetServerSideProps } from 'next';
import { useRouter } from 'next/router';
import api from '../../services/api';
import Link from 'next/link';
import { Container } from '../../styles/pages/Categories';
import Header from '../../components/Header';
import Restaurant from '../../components/pages/Home/Restaurant';
import Empty from '../../components/Empty';
import useWindowDimensions from '../../hooks/useWindowDimensions';
import { useEffect, useState } from 'react';

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

interface ICategoryProps {
  restaurants: IRestaurant[];
}

export default function CategoryList({ restaurants: initialRestaurants }: ICategoryProps) {
  const router = useRouter();
  const { category_slug } = router.query;

  const { width } = useWindowDimensions();
  let windowWidth = width;

  const [restaurants, setRestaurants] = useState<IRestaurant[]>(initialRestaurants);

  useEffect(() => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(async position => {
        const { latitude, longitude } = position.coords;

        const response = await api.get('/restaurants', {
          params: {
            latitude,
            longitude,
            category: category_slug,
          },
        });

        setRestaurants(response.data);
      });
    }
  }, [category_slug]);

  if (router.isFallback) {
    return <p>Carregando...</p>;
  }

  return (
    <>
      <Header title={windowWidth < 960 ? category_slug as string : undefined} isFixed />
      <Container>
        <div>
          <h4>{category_slug} em Belém</h4>
          {restaurants?.length === 0 ? (
            <>
              <Empty />
              <main>
                <h5 className="not-found">
                  Ops... Parece que não há restaurantes :(
                </h5>
              </main>
            </>
          ) : (
            <>
              <ul>
                {restaurants?.map(restaurant => (
                  <li key={restaurant.title}>
                    <Restaurant restaurantData={restaurant} />
                  </li>
                ))}
              </ul>
              <button type="button">Ver mais restaurantes e mercados</button>
            </>
          )}
        </div>
      </Container>
    </>
  );
}

export const getServerSideProps: GetServerSideProps<ICategoryProps> = async context => {
  const { category_slug } = context.params as { category_slug: string };

  // Requisição inicial sem localização
  const response = await api.get('/restaurants', {
    params: {
      category: category_slug,
    },
  });

  const restaurants = response.data;

  return {
    props: {
      restaurants,
    },
  };
};
