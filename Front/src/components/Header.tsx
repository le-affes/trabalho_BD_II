import Link from 'next/link';
import { FiChevronDown, FiChevronLeft, FiSearch } from 'react-icons/fi';
import { Container } from '../styles/components/Header';
import useWindowDimensions from '../hooks/useWindowDimensions';

import Input from './Input';
import Menu from './Menu';
import useGeolocation from '../hooks/useGeolocation';

interface IHeaderProps {
  title?: string | string[] | undefined;
  isFixed?: boolean;
}



export default function Header({ title, isFixed }: IHeaderProps) {
  let coords = { latitude: 0, longitude: 0 };

  const { width } = useWindowDimensions();
  const { address } = useGeolocation();
  return (
    <Container hasTitle={!!title} isFixed={isFixed}>
      {width >= 960 ? (
        <div className="desktop">
          <Link href="/lista-restaurantes">
            <span>
              <svg
                className="logo"
                width="80"
                height="43"
                viewBox="0 0 80 43"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path d="M10 2h2v12h-2V2zM14 2h2v12h-2V2zM18 2h2v12h-2V2zM12 14h4v27h-4V14z" />
                <path d="M26 2c0 5-1 10-3 15v24h4V2h-1z" />
              </svg>
            </span>
          </Link>
          <section className="input-section">
            <Input icon={FiSearch} placeholder="Busque por item ou loja" />
          </section>
          <Menu />
        </div>
      ) : (
        <div className="mobile">
          {!title && <span>Entregar em</span>}
          <main>
            {title ? (
              <>
                <Link href="/lista-restaurantes">
                  <FiChevronLeft color="rgb(55, 0, 117)" size={30} />
                </Link>
                <h3 className="page-title">{title}</h3>
                <p />
              </>
            ) : (
              <>
                <h3>{address}</h3>
                <FiChevronDown color="rgb(55, 0, 117)" size={18} />
              </>
            )}
          </main>
        </div>
      )}
    </Container>
  );
}
